using UnityEngine;
using Firebase.Auth;

namespace Modules.GoogleLogin
{
    public class UserSession : MonoBehaviour
    {
        [Header("Firebase User")]
        // private FirebaseAuth auth;
        // private FirebaseUser user;
        
        public static UserSession Instance { get; set; }
        private FirebaseUser currentUser { get; set; }
        
        private void Start()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
            Debug.Log("UserSession.Awake() called");
        }

        public FirebaseUser GetUser()
        {
            return currentUser;
        }
        
        public void SetUser(FirebaseUser user)
        {
            currentUser = user;
        }
        
        public bool IsLoggedIn()
        {
            return currentUser != null;
        }
    }
}