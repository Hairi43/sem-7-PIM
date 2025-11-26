using Modules.Common.Scripts;
using Modules.ContainerStacker.Scripts.Managers;
using Modules.SaveData.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Modules.Common.Scripts.Managers;

namespace Modules.ContainerStacker.Scripts
{
    public class TowerBuilder : MonoBehaviour
    {
    
        public Rigidbody2D ropeEnd;
        public Transform anchorPos;
        public Camera cam;
        public Button cameraUp;

        [Header("Modifiers")]
        public float maxAngle;
        public float swingMultiplier;
    
        public TowerManager towerManager;
        public TowerCameraManager cameraManager;

        // Start is called once before the first execution of Update after the MonoBehaviour is created

        private string _sceneName = "";
        private int _score;
        private bool _isFirst;

        private void Start()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            _score = 0;
            
            _isFirst = true;
            _sceneName = SceneManager.GetActiveScene().name;
            GameStateService.CreateGameStateBySceneName(_sceneName);
            if (GameStateService.GetPlayedBySceneName(_sceneName)) {
                Time.timeScale = 1;
            }
            towerManager.OnSpawnContainer();
        }

        void FixedUpdate()
        {
            var angle = maxAngle * Mathf.Sin(Time.time * swingMultiplier);
            anchorPos.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        private void OnEnable() {
            Block.OnCollision += OnContainerCollision;
            KillZone.OnContainerOutOfBounds += OnContainerOutOfBounds;
            TutorialPopUp.OnPause += OnTimeStopped;
            TutorialPopUp.OnResume += OnResumed;
        }

        void OnTimeStopped() {
            Time.timeScale = 0;
        }

        void OnResumed() {
            Time.timeScale = 1;
        }
        private void OnDisable()
        {
            Block.OnCollision -= OnContainerCollision;
            KillZone.OnContainerOutOfBounds -= OnContainerOutOfBounds;
            TutorialPopUp.OnPause -= OnTimeStopped;
            TutorialPopUp.OnResume -= OnResumed;
        }

        private void OnContainerOutOfBounds() {
            var isWon = _score >= 10;
            OnGameDone(isWon);
        }

        private void UpdateScore() {
            _score++;
            ScoreManager.ScoreChanged(_score);
            switch (_score)
            {
                case 2:
                    cameraUp.onClick.AddListener(cameraManager.MoveUpByHeight);
                    break;
                case 10:
                    EndOfGameManager.EndGameAvailable();
                    break;
            }
        }
        
        private void OnContainerCollision(Block obj, string collisionTag) {
            switch (collisionTag) {
                case "Floor" when _isFirst:
                    // Debug.Log("Podłoga pierwsza");
                    //TODO - Test if this is actually more fun
                    // _isFirst = false;
                    UpdateScore();
                    break;
                case "Container":
                    // Debug.Log("Kontener");
                    UpdateScore();
                    break;
                case "Floor":
                    // Debug.Log("Podłoga druga");
                    OnContainerOutOfBounds();
                    break;
            }
        }

        private void OnGameDone(bool isWon)
        {
            EndOfGameManager.GameFinished(isWon, _score);
        }
    }
}
