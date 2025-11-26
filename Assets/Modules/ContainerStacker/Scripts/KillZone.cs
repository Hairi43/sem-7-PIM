using System;
using UnityEngine;

namespace Modules.ContainerStacker.Scripts {
    public class KillZone : MonoBehaviour {
        
        public static Action OnContainerOutOfBounds;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Container")) {
                OnContainerOutOfBounds?.Invoke();
                // Debug.Log("You lost!");
            }
        }
    }
}
