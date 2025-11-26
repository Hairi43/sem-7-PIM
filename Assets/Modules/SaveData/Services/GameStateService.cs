#nullable enable
using System.Collections.Generic;
using System.Linq;
using Modules.SaveData.Model;

namespace Modules.SaveData.Services {
    public static class GameStateService
    {
        private static List<GameState> _gameStates => SaveManager.gameData.gameStates;

        private static GameState? GetGameStateBySceneName(string gameName)
        {
            return _gameStates.FirstOrDefault(x => x.gameName == gameName);
        }
        /// <summary>
        /// creates record if not existing
        /// </summary>
        /// <param name="gameName"></param>
        public static void CreateGameStateBySceneName(string gameName)
        {
            var existing = GetGameStateBySceneName(gameName);
            if (existing != null) return;

            var newState = new GameState { gameName = gameName, highScore = 0, isCompleted = false, wasPlayed = false };
            _gameStates.Add(newState);
        }

        /// <summary>
        /// Set highscore only if record exists and is smaller than the new one 
        /// </summary>
        /// <param name="gameName"></param>
        /// <param name="highScore"></param>
        /// <returns></returns>
        public static void SetHighScoreBySceneName(string gameName, int highScore)
        {
            var gs = GetGameStateBySceneName(gameName);
            if (gs != null && gs.highScore < highScore) gs.highScore = highScore;
        }
        public static int GetHighScoreBySceneName(string gameName)
        {
            var gs = GetGameStateBySceneName(gameName);
            if (gs == null)
            {
                return 0;
            }
            return gs.highScore;
        }

        public static bool GetCompletionBySceneName(string gameName)
        {
            return GetGameStateBySceneName(gameName)?.isCompleted ?? false;
        }

        public static bool SetCompletionBySceneName(string gameName, bool isCompleted)
        {
            var gs = GetGameStateBySceneName(gameName);
            if (gs == null) return false;

            gs.isCompleted = isCompleted;
            return true;
        }

        public static bool GetPlayedBySceneName(string gameName)
        {
            return GetGameStateBySceneName(gameName)?.wasPlayed ?? false;
        }

        public static bool SetPlayedBySceneName(string gameName, bool wasPlayed)
        {
            var gs = GetGameStateBySceneName(gameName);
            if (gs == null) return false;

            gs.wasPlayed = wasPlayed;
            return true;
        }
        public static void Initialize()
        {
            string[] sceneNames = { "ColorMixingGame", "quiz", "PaintCatcher", "ContainerStacker", "quiz2" };
            for (int i = 0; i < sceneNames.Length; i++)
             {
                CreateGameStateBySceneName(sceneNames[i]);
             }
        }
    }
}