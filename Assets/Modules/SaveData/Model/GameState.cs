using System;
using Firebase.Firestore;

namespace Modules.SaveData.Model {
    [Serializable]
    [FirestoreData]
    public class GameState {
        [FirestoreProperty] public string gameName  {get; set;}
        [FirestoreProperty] public int highScore {get; set;}
        [FirestoreProperty] public bool isCompleted {get; set;}
        [FirestoreProperty] public bool wasPlayed {get; set;}
        
        public GameState() { }
    }
}