using System;
using System.Collections.Generic;
using Firebase;
using Firebase.Firestore;
using UnityEngine;
using System.Threading.Tasks;
using Firebase.Extensions;
using Modules.GoogleLogin;
using Modules.SaveData.Model;

namespace Modules.SaveData
{
    public class FirestoreManager : MonoBehaviour
    {
        public static FirestoreManager Instance { get; private set; }
        private FirebaseFirestore db;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
            
            DontDestroyOnLoad(gameObject);
            
            db = FirebaseFirestore.DefaultInstance;
            Debug.LogError("Issued connection to database");
        }
        
        public async Task SavePlayerData(GameData gameData)
        {
            Debug.Log("gamedata inside: " + gameData.gameStates);
            try
            {
                // GameData data = JsonUtility.FromJson<GameData>(jsonData);
                DocumentReference docRef = db.Collection("users").Document(UserSession.Instance.GetUser().UserId);
                await docRef.SetAsync(gameData);
                Debug.Log("✅ Dane użytkownika zapisane w Firestore!");
            }
            catch (System.Exception e)
            {
                Debug.LogError("❌ Błąd zapisu danych: " + e.Message);
            }
        }
        
        public async Task<GameData> LoadPlayerData()
        {
            // var user = UserSession.Instance.GetUser();
            DocumentReference docRef = db.Collection("users").Document(UserSession.Instance.GetUser().UserId);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (snapshot.Exists)
            {
                // GameData data = snapshot.ConvertTo<GameData>();
                // string dataString = snapshot.ConvertTo<string>();
                // var jsonData = 
                // GameData gameData = JsonUtility.FromJson<GameData>(snapshot);
                Debug.Log("Dane użytkownika wczytane z Firestore!");
                return snapshot.ConvertTo<GameData>();
            }
            Debug.Log("Brak danych dla użytkownika.");
            return null;
        }
    }
}