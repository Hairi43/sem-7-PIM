using System.Collections.Generic;
using UnityEngine;

namespace Modules.ContainerStacker.Scripts.Managers {
    public class TowerManager : MonoBehaviour
    {
        public GameObject prefab;
        public Transform ropeEnd;
        public int containersTracked = 3;
        private GameObject _currentContainer;
        private readonly Queue<GameObject> _containers = new();
        

        public void OnSpawnContainer()
        {
            _currentContainer = Instantiate(prefab, ropeEnd.position, ropeEnd.rotation, parent: ropeEnd);
        }
        private void Update() {
        
            if (!_currentContainer) return;
        
            var followPosition = ropeEnd.position;
            _currentContainer.transform.position = followPosition;
        }

        private void Enqueue(GameObject container)
        {
            _containers.Enqueue(container);

            if (_containers.Count > containersTracked)
            {
                var oldest = _containers.Dequeue();
                // oldest.tag = "Untagged";
                
                var last = _containers.Peek();
                last.tag = "Untagged";
                var rb = last.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    DisableSimulation(rb);
                }
                Destroy(oldest);
            }
        }
        
        private void DisableSimulation(Rigidbody2D rigidBody)
        {
            rigidBody.bodyType = RigidbodyType2D.Static;
        }
    
        public void OnContainerRelease() {
            if (_currentContainer) {
                _currentContainer.tag = "Container";
                _currentContainer.transform.parent = null;
                Enqueue(_currentContainer);
                _currentContainer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                _currentContainer = null;
            }
            Invoke(nameof(OnSpawnContainer), 1.0f);
        }
    }
}
