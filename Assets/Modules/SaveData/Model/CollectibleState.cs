using System;
using Firebase.Firestore;

namespace Modules.SaveData.Model {
    [Serializable]
    [FirestoreData]
    public class CollectibleState {
        [FirestoreProperty] public string nameId {get; set;}
        [FirestoreProperty] public bool isCollected  {get; set;}
        
        public CollectibleState() { }
    }
}