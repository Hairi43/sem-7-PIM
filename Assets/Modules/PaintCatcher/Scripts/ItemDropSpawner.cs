using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; // Dodaj ten using
// Można używać też System.Random, działa git imo lepiej

namespace Modules.PaintCatcher.Scripts {
    public class ItemDropSpawner : MonoBehaviour
    {
        public GameObject items; // Przypisz prefabrykowane kropelki w inspektorze
        public ItemCatcher itemCatcher;
        public float spawnInterval = 0.01f;
        private bool _isSpawning; // Flaga do kontrolowania spawnowania
        private readonly List<GameObject> _activeBombs = new List<GameObject>(); // Lista aktywnych kropli

        public void SpawnStart()
        {
            _isSpawning = true; // Ustaw flagę spawnowania
            InvokeRepeating(nameof(SpawnItem), 0f, spawnInterval); // Uruchom spawnowanie
        }

        public void SpawnRestart()
        {
            if (_isSpawning)
            {
                CancelInvoke(nameof(SpawnItem)); // Zatrzymaj spawnowanie
                _isSpawning = false; // Zresetuj flagę
            }
            SpawnStart(); // Uruchom spawnowanie ponownie
        }

        //TODO - Dopracować działanie na innych rozdzielczościach i poprawić
        void SpawnItem()
        {
            // float randomX = Random.Range(0, 1700); // Dostosuj wartości do swojego ekranu
            float coordX = Random.Range(299, Screen.width - 299);
            var item = Instantiate(items, new Vector3(coordX, 1280, 0), Quaternion.identity, transform);
            _activeBombs.Add(item); 
        }

        public void ClearActiveBombs()
        {
            foreach (var bom in _activeBombs)
            {
                Destroy(bom); // Zniszcz każdą kroplę
            }
            _activeBombs.Clear(); // Wyczyść listę
        }
    }
}