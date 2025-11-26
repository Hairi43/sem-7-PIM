using System.Collections.Generic;
using System.Linq;
using Modules.SaveData.Controllers;
using Modules.SaveData.Services;
using UnityEngine;

namespace Modules.MAP.Scripts {
    public class LocationController : MonoBehaviour {
        public GameObject monstersLocations;
        public GameObject factsLocations;
        public Color standardColor;
        public Color collectedColor;
    
        private SpriteRenderer[] _monstersLocations;
        private SpriteRenderer[] _factsLocations;
        private CollectiblesController _instance;
        
        private void Start()
        {
            _instance = CollectiblesController.Instance;

            // Get top-level children as location groups
            var monsterLocationRoots = GetDirectChildren(monstersLocations);
            var factLocationRoots = GetDirectChildren(factsLocations);

            CheckCollected(monsterLocationRoots, _instance.Creature);
            CheckCollected(factLocationRoots, _instance.Facts);
        }

        private void CheckCollected(Transform[] locations, ICollectibleService collectibleService)
        {
            for (var i = 0; i < locations.Length; i++)
            {
                var id = (i + 1).ToString();
                var targetColor = collectibleService.IsCollected(id) ? collectedColor : standardColor;

                // Apply color to all SpriteRenderers in this location group
                ChangeColorRecursively(locations[i].gameObject, targetColor);
            }
        }

        private static void ChangeColorRecursively(GameObject root, Color newColor)
        {
            var renderers = root.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (var sr in renderers)
            {
                sr.color = newColor;
            }
        }

        private Transform[] GetDirectChildren(GameObject parent)
        {
            return parent.transform.Cast<Transform>().ToArray();
        }
    }
}
