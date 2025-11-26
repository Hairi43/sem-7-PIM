using System;
using Modules.SaveData;
using Modules.SaveData.Controllers;
using Modules.SaveData.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Common.Scripts.Managers
{
    public static class EndOfGameManager
    {
        public static event Action<bool, int> OnGameFinished;
        public static event Action OnEndGameAvailable;
    
        /// <summary>
        /// Process game end and send signal to ui for screen pop up
        /// </summary>
        /// <param name="isWon">Did the player won the game or not</param>
        /// <param name="score">If there is no score in your game leave as 0</param>
        public static void GameFinished(bool isWon, int score)
        {
            var sceneName = SceneManager.GetActiveScene().name;
            GameStateService.SetPlayedBySceneName(sceneName, true);
            GameStateService.SetHighScoreBySceneName(sceneName, score);
            OnGameFinished?.Invoke(isWon, score);
            if (!isWon)
            {
                SaveManager.Save();
                return;
            }

            if (GameStateService.GetCompletionBySceneName(sceneName) == false)
            {
                GameStateService.SetCompletionBySceneName(sceneName, true);
                ProcessCollection();
            }
            
            SaveManager.Save();
        }

        /// <summary>
        /// Call to signal possibility for the game to be ended manually
        /// </summary>
        public static void EndGameAvailable()
        {
            OnEndGameAvailable?.Invoke();
        }
        /// <summary>
        /// Hate it,
        ///Kill it,
        ///better with fire
        ///                  ~Hubert, its creator btw
        /// </summary>
        private static void ProcessCollection()
        {
            //first monster has index 1
            const int firstGame = 5;
            var index = SceneManager.GetActiveScene().buildIndex - firstGame;
            Debug.Log($"index:{index} firstGame:{firstGame}");
            
            PlayerPrefs.SetString("lastMonster", $"{index}");
            CollectiblesController.Instance.Creature.Collect($"{index}");
        }
    }
}
