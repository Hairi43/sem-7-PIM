using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Unity.XR.CoreUtils;
using ZXing;
using TMPro;
using Modules.SaveData;
using Modules.SaveData.Services;
using Modules.SaveData.Services.Modules.SaveData.Services;


/// WORKS !!!


namespace MyProject
{
    /// <summary>
    /// Download zxing.unity.dll from
    /// https://github.com/micjahn/ZXing.Net/tree/master/Clients/UnityDemo/Assets
    /// and put it into Assets/Plugins/
    /// 
    /// Put this script on the AR Camera in your ARFoundation script and reference the ARCameraManager script
    ///
    /// Info about how this script was put together:
    /// TryAcquireLatestCpuImage logic via 
    /// https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.2/manual/cpu-camera-image.html#asynchronously-convert-to-grayscale-and-color
    /// 
    /// Using image data for QR code reader via
    /// https://github.com/Unity-Technologies/arfoundation-samples/issues/489#issuecomment-1193087198
    /// </summary>
    public class QRCodeReader : MonoBehaviour
    {
        [SerializeField]
        private ARCameraManager _arCameraManager;
        [SerializeField]
        private int _scanQRCodeEveryXFrame = 30;

        [SerializeField]
        private bool _isActive = true;
        private IBarcodeReader _barcodeReader;
        private bool _isProcessing = false;
        private XRCpuImage _currentCPUImage;

        [SerializeField]
        private bool _isShowDebugImage = false;
        private Texture2D _texture2D;
        private Rect _screenRect;


        string QrCode = string.Empty;
        public TMP_Text output;
        // [SerializeField]
        // public GameObject plane;

        // [SerializeField]
        // private bool wasModelsActivated = false;

        [SerializeField] GameObject planePrefab;
        private GameObject spawnedPlane;


        // [SerializeField] XROrigin xROrigin;
        // game scenes
        // enum Games
        // {
        //     quiz,
        //     paintCatcher,
        // }

        [SerializeField] private Transform xrOriginTransform;

        [SerializeField] private ARAnchorManager anchorManager;


        // notes prefabs
        public GameObject[] prefabs;

        CreatureService _creatureService;
        bool isCreated = false;

        GameObject spawnedNote;

        // monster prefabs
        public GameObject[] monsterPrefabs;
        bool isMonsterCreated = false;
        GameObject spawnedMonster;



        // private ARAnchorManager.AnchorCreationResult resultCreateAnchor;

        void Start()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            _barcodeReader = new BarcodeReader();
            // _screenRect = new Rect(0, 0, Screen.width, Screen.height);
            _screenRect = new Rect(0, 0, 512, 512);
            // _screenRect = new Rect(0, 0, 256, 256);

            // doesn't remove models in AR after scene change
            // DontDestroyOnLoad(plane); 

            if (!isCreated)
            {
                _creatureService = new CreatureService();
                isCreated = true;
            }

            Debug.Log("opened tutorial: " + PlayerPrefs.GetString("openedTutorialQR", ""));

            if (PlayerPrefs.GetString("openedTutorialQR", "") != "true")
            {
                PlayerPrefs.SetString("openedTutorialQR", "true");
                PopupNotification.Instance.OpenOnce();
            }

            Debug.Log("[QRCodeReader] Start. camera position - : " + Camera.main.transform.position);
            Debug.Log("[QRCodeReader] Start. camera rotation - : " + Camera.main.transform.rotation);

            // Jeśli po powrocie ze sceny gry chcemy pokazać obiekt względem zeskanowanego QR
            // if (QRDataHolder.Instance != null && QRDataHolder.Instance.spawnNoteAfterGame)
            // {
            //     SpawnObjectRelativeToQR();
            //     QRDataHolder.Instance.spawnNoteAfterGame = false;
            // }

        }

        void Update()
        {
            if (!_isActive)
                return;

            if ((Time.frameCount % _scanQRCodeEveryXFrame) == 0 && !_isProcessing)
            {
                if (_arCameraManager.TryAcquireLatestCpuImage(out _currentCPUImage))
                {
                    _isProcessing = true;
                    StartCoroutine(ProcessImage(_currentCPUImage));
                    _currentCPUImage.Dispose();
                }
            }
        }

        void SetIsActive(bool state)
        {
            _isActive = state;
        }

        void OnGUI()
        {
            if (_isShowDebugImage)
                // GUI.DrawTexture(_screenRect, _texture2D, ScaleMode.ScaleToFit);
                GUI.DrawTexture(_screenRect, _texture2D);
        }

        IEnumerator ProcessImage(XRCpuImage image)
        {
            Debug.LogWarning("ProcessQRCode image.dimensions: " + image.dimensions);

            // Create the async conversion request.
            var request = image.ConvertAsync(new XRCpuImage.ConversionParams
            {
                // Image dimentions on Galaxy S22 are 480x640 - width x height

                // Use the full image.
                // inputRect = new RectInt(0, 0, image.width, image.height),
                inputRect = new RectInt((image.width / 2) - 90, (image.height / 2) - 90, 180, 180),
                // inputRect = new RectInt((image.width / 2) - 70, (image.height / 2) - 70, 140, 140),

                // Downsample by 2.
                //outputDimensions = new Vector2Int(image.width / 2, image.height / 2),
                outputDimensions = new Vector2Int(180, 180),
                // outputDimensions = new Vector2Int(120, 120),

                // Color image format.
                outputFormat = TextureFormat.R8,

                // Flip across the Y axis.
                transformation = XRCpuImage.Transformation.MirrorY
            });



            // Wait for the conversion to complete.
            while (!request.status.IsDone())
                yield return null;

            // Check status to see if the conversion completed successfully.
            if (request.status != XRCpuImage.AsyncConversionStatus.Ready)
            {
                // Something went wrong.
                Debug.LogWarningFormat("ProcessImage: Request failed with status {0}", request.status);

                // Dispose even if there is an error.
                request.Dispose();
                yield break;
            }

            // Image data is ready. Let's apply it to a Texture2D.
            var rawData = request.GetData<byte>();


            if (_isShowDebugImage)
            {
                // Create a texture if necessary.
                if (_texture2D == null)
                {
                    _texture2D = new Texture2D(
                        request.conversionParams.outputDimensions.x,
                        request.conversionParams.outputDimensions.y,
                        request.conversionParams.outputFormat,
                        false);
                }

                // Copy the image data into the texture.
                _texture2D.LoadRawTextureData(rawData);
                _texture2D.Apply();
            }


            byte[] grayscalePixels = NativeArrayExtensions.ToRawBytes(rawData);
            // The 'grayscalePixels' array now contains the image data.
            // var result = _barcodeReader.Decode(grayscalePixels, image.width, image.height, RGBLuminanceSource.BitmapFormat.Gray8);
            var result = _barcodeReader.Decode(grayscalePixels, 180, 180, RGBLuminanceSource.BitmapFormat.Gray8);
            // var result = _barcodeReader.Decode(grayscalePixels, 120, 120, RGBLuminanceSource.BitmapFormat.Gray8);
            // If the result is not null, we set the browserOpener's string to match the text from result.
            if (result != null)
            {
                // string gameId = result.Text.Substring(result.Text.LastIndexOf('/') + 1);
                // SceneManager.LoadScene("game_scene_" + gameId);
                // Debug.Log(result.Text);
                // output.text = result.Text;

                string gameId = result.Text.Substring(result.Text.LastIndexOf('/') + 1);
                
                QRDataHolder.Instance.gameId = gameId;
                var sceneName = SceneManager.GetActiveScene().name;
                PlayerPrefs.SetString("lastScene", sceneName);
                PlayerPrefs.Save();

                // if (spawnedNote.activeSelf)
                // {
                //     Debug.Log("[QR] Destroyed spawnedNote");
                //     Destroy(spawnedNote);
                // }
                
                switch (gameId)
                {
                    case "0":
                        // Debug.Log("[QR] inside case 0");
                        // potem zmienić na isCompleted
                        bool isPlayed0 = GameStateService.GetPlayedBySceneName("ColorMixingGame");                    
                        
                        SceneManager.LoadScene("ColorMixingGame");
                        

                        // PlayerPrefs.SetString("lastMonster", "Monster1");

                        // QRDataHolder.Instance.prefabOffset = new Vector3(0.5f, 0.6f, 0.0f);
                        // Pose qrPose1 = new Pose(_arCameraManager.transform.position + _arCameraManager.transform.forward * 1.0f,
                        //                         _arCameraManager.transform.rotation);

                        // CreateAnchor(qrPose1);

                        // QRDataHolder.Instance.spawnNoteAfterGame = true;
                        
                        break;

                    case "1":
                        bool isPlayed1 = GameStateService.GetPlayedBySceneName("PaintCatcher");                      

                        SceneManager.LoadScene("PaintCatcher");

                        // PlayerPrefs.SetString("lastMonster", "Monster1");

                        // QRDataHolder.Instance.prefabOffset = new Vector3(0.5f, 0.6f, 0.0f);
                        // Pose qrPose1 = new Pose(_arCameraManager.transform.position + _arCameraManager.transform.forward * 1.0f,
                        //                         _arCameraManager.transform.rotation);

                        // CreateAnchor(qrPose1);

                        QRDataHolder.Instance.spawnNoteAfterGame = true;
                        
                        break;

                    case "2":
                        bool isPlayed2 = GameStateService.GetPlayedBySceneName("quiz");
                        
                        SceneManager.LoadScene("quiz");
                        // // _creatureService.Collect("Monster2"); // zbieranie jest w PopUpMessageMonster Start()
                        // PlayerPrefs.SetString("lastMonster", "Monster2");

                        // QRDataHolder.Instance.prefabOffset = new Vector3(-0.5f, 0.6f, 0.0f);
                        // Pose qrPose2 = new Pose(_arCameraManager.transform.position + _arCameraManager.transform.forward * 1.0f,
                        //                         _arCameraManager.transform.rotation);

                        // CreateAnchor(qrPose2);

                        // QRDataHolder.Instance.spawnNoteAfterGame = true;
                        
                        break;

                    case "3":
                        bool isPlayed3 = GameStateService.GetPlayedBySceneName("ContainerStacker");
                        
                        SceneManager.LoadScene("ContainerStacker");
                        // _creatureService.Collect("Monster3");
                        // PlayerPrefs.SetString("lastMonster", "Monster3");

                        // QRDataHolder.Instance.prefabOffset = new Vector3(0.5f, 0.6f, 0.0f);
                        // Pose qrPose3 = new Pose(_arCameraManager.transform.position + _arCameraManager.transform.forward * 1.0f,
                        //                         _arCameraManager.transform.rotation);

                        // CreateAnchor(qrPose3);

                        // QRDataHolder.Instance.spawnNoteAfterGame = true;
                        
                        break;
                    case "4":
                        bool isPlayed4 = GameStateService.GetPlayedBySceneName("quiz2");
                        
                        SceneManager.LoadScene("quiz2");

                        // // _creatureService.Collect("Monster4");
                        // PlayerPrefs.SetString("lastMonster", "Monster4");

                        // QRDataHolder.Instance.prefabOffset = new Vector3(-0.5f, 0.6f, 0.0f);
                        // Pose qrPose4 = new Pose(_arCameraManager.transform.position + _arCameraManager.transform.forward * 1.0f,
                        //     _arCameraManager.transform.rotation);

                        // CreateAnchor(qrPose4);

                        // QRDataHolder.Instance.spawnNoteAfterGame = true;
                        
                        break;

                    default:
                        Debug.LogWarning("Nieznane gameId: " + gameId);
                        break;
                }
            }
            else
            {
                // Debug.Log("Failed result debug");
                // output.text = "Failed result";
                // output.text = "";
            }


            // Need to dispose the request to delete resources associated
            // with the request, including the raw data.
            request.Dispose();


            _isProcessing = false;
        }

        // async Task<ARAnchor> CreateAnchor(Pose pose)
        // {
        //     return await anchorManager.TryAddAnchorAsync(pose);
        // }
        async void CreateAnchor(Pose pose)
        {
            var result = await anchorManager.TryAddAnchorAsync(pose);
            QRDataHolder.Instance.qrAnchor = result.value;
        }

        // async void CreateAnchorMonster(Pose pose)
        // {
        //     var result = await anchorManager.TryAddAnchorAsync(pose);
        //     QRDataHolder.Instance.qrAnchorMonster = result.value;
        // }

        void SpawnObjectRelativeToQR(int noteID)
        {
            if (prefabs.Length == 0)
            {
                Debug.LogWarning("[QR] No prefabs assigned!");
                return;
            }

            GameObject chosenPrefab = prefabs[noteID];
            // if (noteID == -1)
            // {
            //     // Wybieramy prefab losowo (lub możesz zmienić na konkretny)
            //     int randomIndex = UnityEngine.Random.Range(0, prefabs.Length);
            //     chosenPrefab = prefabs[randomIndex];
            // }

            

            // Pozycja kamery i jej forward
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            Vector3 cameraUp = Camera.main.transform.up;

            // Obliczamy pozycję QR jako 1m przed kamerą + zapisany offset (w lokalnych osiach kamery)
            Vector3 qrWorldPosition = cameraPosition + cameraForward * 1.0f;  // odległość QR 1 metr

            Vector3 offset = QRDataHolder.Instance.prefabOffset;

            Vector3 spawnPosition = qrWorldPosition +
                                    cameraRight * offset.x +
                                    cameraUp * offset.y +
                                    cameraForward * offset.z;

            // // Obrót obiektu - ustawiamy tak, aby był skierowany w stronę kamery (albo możesz zmienić)
            // Vector3 lookDirection = cameraPosition - spawnPosition;
            // lookDirection.y = 0; // opcjonalnie, aby nie przechylać w pionie
            // Quaternion spawnRotation = lookDirection != Vector3.zero
            //     ? Quaternion.LookRotation(lookDirection)
            //     : Quaternion.identity;

            Vector3 lookDir = cameraPosition - spawnPosition;
            lookDir.y = 0;
            Quaternion spawnRotation = Quaternion.LookRotation(lookDir);

            // Korekcja obrotu, np. gdy prefab patrzy w +X zamiast +Z
            Quaternion correction = Quaternion.Euler(0, 90, 0);
            spawnRotation *= correction;

            // Tworzymy obiekt
            spawnedNote = Instantiate(chosenPrefab, spawnPosition, spawnRotation);
            Debug.Log("[QR] Spawned prefab relative to camera + offset: " + spawnPosition);
        }


        // void SpawnMonster(int monsterID, Pose pose)
        // {
        //     if (monsterPrefabs.Length == 0)
        //     {
        //         Debug.LogWarning("[QR] No monster prefabs assigned!");
        //         return;
        //     }

        //     CreateAnchorMonster(pose);

        //     GameObject chosenPrefab = monsterPrefabs[monsterID];
         
        //     // Pozycja kamery i jej forward
        //     Vector3 cameraPosition = Camera.main.transform.position;
        //     Vector3 cameraForward = Camera.main.transform.forward;
        //     Vector3 cameraRight = Camera.main.transform.right;
        //     Vector3 cameraUp = Camera.main.transform.up;

        //     // Obliczamy pozycję QR jako 1m przed kamerą + zapisany offset (w lokalnych osiach kamery)
        //     Vector3 qrWorldPosition = cameraPosition + cameraForward * 1.0f;  // odległość QR 1 metr

        //     Vector3 offset = QRDataHolder.Instance.monsterOffset * 7;

        //     Vector3 spawnPosition = qrWorldPosition +
        //                             cameraRight * offset.x +
        //                             cameraUp * offset.y +
        //                             cameraForward * offset.z;

        //     Vector3 lookDir = cameraPosition - spawnPosition;
        //     lookDir.y = 0;
        //     // Quaternion spawnRotation = Quaternion.LookRotation(lookDir);

        //     Vector3 baseEuler = chosenPrefab.transform.rotation.eulerAngles;
        //     float targetY = Quaternion.LookRotation(lookDir).eulerAngles.y - 90f;

        //     // Korekcja obrotu, np. gdy prefab patrzy w +X zamiast +Z
        //     Quaternion finalRotation = Quaternion.Euler(baseEuler.x, targetY, baseEuler.z);
        //     // spawnRotation *= correction;

        //     // Tworzymy obiekt
        //     spawnedMonster = Instantiate(chosenPrefab, spawnPosition, finalRotation);
        //     // spawnedMonster.transform.rotation = Quaternion.LookRotation(lookDir) * correction;
        //     Debug.Log("[QR] Spawned prefab relative to camera + offset: " + spawnPosition);
        // }


        void SpawnMonster(int monsterID, Pose pose)
        {
            if (monsterPrefabs.Length == 0)
            {
                Debug.LogWarning("[QR] No monster prefabs assigned!");
                return;
            }

            CreateAnchor(pose);

            GameObject chosenPrefab = monsterPrefabs[monsterID];
         
            // Pozycja kamery i jej forward
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            Vector3 cameraUp = Camera.main.transform.up;

            // Obliczamy pozycję QR jako 1m przed kamerą + zapisany offset (w lokalnych osiach kamery)
            Vector3 qrWorldPosition = cameraPosition + cameraForward * 1.0f;  // odległość QR 1 metr

            Vector3 offset = QRDataHolder.Instance.monsterOffset * 7;

            Vector3 spawnPosition = qrWorldPosition +
                                    cameraRight * offset.x +
                                    cameraUp * offset.y +
                                    cameraForward * offset.z;

            Vector3 lookDir = cameraPosition - spawnPosition;
            lookDir.y = 0;
            // Quaternion spawnRotation = Quaternion.LookRotation(lookDir);

            // Vector3 baseEuler = chosenPrefab.transform.rotation.eulerAngles;
            // float targetY = Quaternion.LookRotation(lookDir).eulerAngles.y - 90f;

            // Korekcja obrotu, np. gdy prefab patrzy w +X zamiast +Z
            // Quaternion finalRotation = Quaternion.Euler(baseEuler.x, targetY, baseEuler.z);
            // spawnRotation *= correction;

            // Tworzymy obiekt
            spawnedMonster = Instantiate(chosenPrefab, spawnPosition, chosenPrefab.transform.rotation);
            // Odczytaj rotację prefab dopiero teraz
            Vector3 baseEuler = spawnedMonster.transform.rotation.eulerAngles;

            float targetY = Quaternion.LookRotation(lookDir).eulerAngles.y + 90f;
            // special case for red monster, because idk what's going on
            if (monsterID == 1)
            {
                targetY = Quaternion.LookRotation(lookDir).eulerAngles.y - 90f;
            }

            Quaternion finalRotation = Quaternion.Euler(baseEuler.x, targetY, baseEuler.z);

            // Ustaw rotację po instancjacji
            spawnedMonster.transform.rotation = finalRotation;
            // spawnedMonster.transform.rotation = Quaternion.LookRotation(lookDir) * correction;
            Debug.Log("[QR] Spawned prefab relative to camera + offset: " + spawnPosition);
        }

        IEnumerator LoadSceneDelayed(string sceneName)
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(sceneName);
        }

    }

    

    /// <summary>
    /// https://gist.github.com/asus4/992ae563ca1263b1dd936b970e7fc206
    /// </summary>
    public static class NativeArrayExtensions
    {
        public static byte[] ToRawBytes<T>(this NativeArray<T> arr) where T : struct
        {
            var slice = new NativeSlice<T>(arr).SliceConvert<byte>();
            var bytes = new byte[slice.Length];
            slice.CopyTo(bytes);
            return bytes;
        }
    }
}