using System.Collections.Generic;
using Firebase.Firestore;

namespace Modules.SaveData.Model {
    [System.Serializable]
    [FirestoreData]
    public class GameData
    {
        [FirestoreProperty]
        public List<GameState> gameStates { get; set; }

        [FirestoreProperty]
        public List<CollectibleState> facts { get; set; }

        [FirestoreProperty]
        public List<CollectibleState> creatures { get; set; }

        public GameData()
        {
            gameStates = new List<GameState>();
            facts = new List<CollectibleState>();
            creatures = new List<CollectibleState>();
        }
    }
}