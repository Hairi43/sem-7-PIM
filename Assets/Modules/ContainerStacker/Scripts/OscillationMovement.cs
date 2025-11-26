using UnityEngine;

namespace Modules.ContainerStacker.Scripts {
    public class OscillationMovement : MonoBehaviour
    {
        public float speed = 1.0f;          // Speed of movement
        public float distance = 2.0f;       // Max distance from starting position
        private Vector3 _startPosition;
        void Start()
        {
            _startPosition = transform.localPosition; // Use localPosition to avoid conflicts with parent movement
        }
        void Update()
        {
            float xOffset = Mathf.Sin(Time.time * speed) * distance;
            transform.localPosition = _startPosition + new Vector3(xOffset, 0, 0);
        }
    }
}
