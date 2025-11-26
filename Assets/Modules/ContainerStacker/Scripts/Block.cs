using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.ContainerStacker.Scripts {
    public class Block : MonoBehaviour
    {
        private Camera _cam;
        private bool _isCounted;
        public static Action<Block, string> OnCollision;

        private Color[] _colors = {
            new (0.65f, 0.13f, 0.13f), // Dark Red / Maroon
            new (0.0f, 0.5f, 0.0f), // Green
            new (0.0f, 0.0f, 0.8f), // Blue
            new (0.5f, 0.5f, 0.5f), // Gray
            new (1.0f, 0.55f, 0.0f), // Orange
            new (0.75f, 0.75f, 0.0f), // Yellow
            new (0.36f, 0.2f, 0.09f), // Brown
            new (0.25f, 0.25f, 0.25f) // Dark Gray;
        };
    
    
        private void Start()
        {
            _cam = Camera.main;
            GetComponent<Rigidbody2D>();
            var r = Random.Range(0, _colors.Length);
            var color = _colors[r];
            GetComponent<SpriteRenderer>().color = color;
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (_isCounted) return;
        
            // if (collision.collider.CompareTag("Container")) {
            //     _isCounted = true;
            //     OnCollision?.Invoke(this, "Container");
            // }

            if (collision.collider.CompareTag("Floor")) {
                _isCounted = true;
                OnCollision?.Invoke(this, "Floor");
            }

        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (_isCounted) return;
            if (other.CompareTag("Container")) {
                _isCounted = true;
                OnCollision?.Invoke(this,"Container");
            }
        }
    }
}
