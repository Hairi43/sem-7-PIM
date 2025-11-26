using UnityEngine;

namespace Modules.ContainerStacker.Scripts {
    public class ParallaxEffectController : MonoBehaviour
    {
        public Transform cam; //Main Camera
        public Vector3 camStartPos;
        public float distance;

        public GameObject[] backgrounds;
        public Material[] mat;
        public float[] backSpeed;

        public float farthestBack;

        [Range(0.01f, 0.05f)]
        public float parallaxSpeed;

        void Start()
        {
            cam = Camera.main.transform;
            camStartPos = cam.position;

            int backCount = transform.childCount;
            mat = new Material[backCount];
            backSpeed = new float[backCount];
            backgrounds = new GameObject[backCount];

            for (int i = 0; i < backCount; i++)
            {
                backgrounds[i] = transform.GetChild(i).gameObject;
                mat[i] = backgrounds[i].GetComponent<Renderer>().material;
            }

            BackSpeedCalculate(backCount);
        }

        void BackSpeedCalculate(int backCount)
        {
            for (int i = 0; i < backCount; i++)
            {
                float depth = backgrounds[i].transform.position.z - cam.position.z;
                if (depth > farthestBack)
                    farthestBack = depth;
            }

            for (int i = 0; i < backCount; i++)
            {
                float depth = backgrounds[i].transform.position.z - cam.position.z;
                backSpeed[i] = 1 - (depth / farthestBack);
            }
        }

        void LateUpdate()
        {
            distance = cam.position.y - camStartPos.y;

            transform.position = new Vector3(transform.position.x, cam.position.y, 0);

            for (int i = 0; i < backgrounds.Length; i++)
            {
                float speed = backSpeed[i] * parallaxSpeed;

                Vector2 offset = new Vector2(0, distance * speed);
                mat[i].SetTextureOffset("_MainTex", offset);
            }
        }
    }
}
