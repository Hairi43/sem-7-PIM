using UnityEngine;
using Firebase.Auth;


// TODO is this class necessary?
namespace Modules.GoogleLogin
{
    public class LoginManager : MonoBehaviour
    {
        private FirebaseAuth auth;

        private void Start()
        {
            auth = FirebaseAuth.DefaultInstance;
        }

        public void OnLoginSuccess(FirebaseUser user)
        {
            Debug.Log("Zalogowano jako: " + user.DisplayName);
            UserSession.Instance.SetUser(user);
        }
    }
}