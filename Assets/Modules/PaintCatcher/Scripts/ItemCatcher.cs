using Modules.Common.Scripts;
using Modules.Common.Scripts.Managers;
using Modules.SaveData.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.PaintCatcher.Scripts {
    public class ItemCatcher : MonoBehaviour {
        public bool gameStart;
        public ItemDropSpawner itemDropSpawner; // Dodaj odniesienie do spawnera
        public CartController cartController;
        private string _sceneName;
        private int _score;

        void Start() {
            _sceneName = SceneManager.GetActiveScene().name;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            ScoreManager.ScoreChanged(_score);
            GameStateService.CreateGameStateBySceneName(_sceneName);
            gameStart = true;
            if (GameStateService.GetPlayedBySceneName(_sceneName)) {
                Time.timeScale = 1;
            }
            // doda się tutorial pewnie w przyszłości
            //TODO - Zaprojektowanie ekranu poradnika do gier
            //TODO - dodanie wywołania go tutaj (raczej prosty sygnał)

            // Uruchomienie spawnowania kropel
            if (itemDropSpawner != null) {
                itemDropSpawner.SpawnStart(); // Wywołaj metodę uruchamiającą spawnowanie
            }
        }

        public void AddScore() {
            _score++;
            ScoreManager.ScoreChanged(_score);
            if (_score >= 10) EndOfGameManager.EndGameAvailable();
        }

        public void GameOver() {
            EndOfGameManager.GameFinished(_score >= 10, _score);
        }

        void OnPause() {
            Time.timeScale = 0;
        }

        void OnResume() {
            Time.timeScale = 1;
        }

        private void OnEnable() {
            TutorialPopUp.OnPause += OnPause;
            TutorialPopUp.OnResume += OnResume;
        }

        private void OnDisable() {
            TutorialPopUp.OnPause -= OnPause;
            TutorialPopUp.OnResume -= OnResume;
        }

        public void RestartGame() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}