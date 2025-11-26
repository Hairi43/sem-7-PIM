using System;
using System.IO;
using Modules.GoogleLogin;
using Modules.SaveData.Model;
using UnityEngine;

namespace Modules.SaveData {
    public static class SaveManager {
        // private static readonly string FilePath = Application.persistentDataPath + "/GameData.json";
    
        public static GameData gameData { get; set; } = new GameData();
        
        public static void ClearData()
        {
            gameData = new GameData();
        }
        
        public static async void Save() {
            // var jsonData = JsonUtility.ToJson(gameData);
            // File.WriteAllText(FilePath, jsonData);
            
            // Firestore save data to database
            if (FirestoreManager.Instance != null)
            {
                Debug.Log("FirestoreManager save jest gotowy.");
            }
            else
            {
                Debug.Log("FirestoreManager save nie jest gotowy.");
                // FirestoreManager.Instance.SavePlayerData(gameData);
            }
            Debug.Log(JsonUtility.ToJson(gameData));
            await FirestoreManager.Instance.SavePlayerData(gameData);
            
        }
    
        public static async void Load() {
            // if (File.Exists(FilePath)) {
            //     // var jsonData = File.ReadAllText(FilePath);
            //     // gameData = JsonUtility.FromJson<GameData>(jsonData);
            // } else {
            //     //might add default state for this thingy
            //     //that, or we create inside services
            //     
            //     // TODO temporary commented for database test - uncomment
            //     gameData = new GameData();
            //     Save();
            // }
            // if (FirestoreManager.Instance == null)
            // {
            //     gameData = new GameData();
            //     Save();
            // }
            
            GameData receivedGameData = await FirestoreManager.Instance.LoadPlayerData();
            Debug.Log("receivedGameData: " + receivedGameData);
            if (receivedGameData != null)
            {
                gameData = receivedGameData;
                Debug.Log("[SaveManager] Load GameData.json");
                Debug.Log(JsonUtility.ToJson(gameData));
                // try
                // {
                //     Debug.Log("[SaveManager] GameData.gameStates count" + gameData.gameStates[0].gameName);
                //     Debug.Log("[SaveManager] GameData.creatures count" + gameData.creatures[0].nameId);
                //     Debug.Log("[SaveManager] GameData.facts count" + gameData.facts[0].nameId);
                // }
                // catch (Exception e)
                // {
                //     Debug.Log("[SaveManager] Error loading GameData.json " + e);
                // }
                
            }
            // else
            // {
            //     gameData = new GameData();
            //     Save();
            // }
            // Debug.Log("[SaveManager] Load GameData.json]");
            // Firestore load data from database
            // if (FirestoreManager.Instance != null)
            // {
                
                // var remoteData = await FirestoreManager.Instance.LoadPlayerData(UserSession.Instance.GetUser().UserId);
                // if (remoteData != null)
                // {
                //     gameData = JsonUtility.FromJson<GameData>(remoteData);
                //     Debug.Log("Wczytano dane z Firestore!");
                // }
            // }
            // else
            // {
            //     Debug.Log("FirestoreManager load nie jest gotowy.");
            //     // gameData = await FirestoreManager.Instance.LoadPlayerData();
            // }
        }
    }
}
