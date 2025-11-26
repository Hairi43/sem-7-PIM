using Firebase.Extensions;
using Google;
using UnityEngine;
using TMPro;
using Firebase.Auth;
using Modules.GoogleLogin;
using Modules.SaveData;
using UnityEngine.SceneManagement;

public class LoginWithGoogle : MonoBehaviour
{
    [Header("Google API")]
    // WebClientID on Firebase - don't add on Github
    private string GoogleAPI = "334526042981-573293innsma2kim638u6gk70b38metr.apps.googleusercontent.com";
    private GoogleSignInConfiguration configuration;

    [Header("Firebase Auth")]
    private FirebaseAuth auth;
    private FirebaseUser user;

    [Header("UI References")]
    public TextMeshProUGUI Username, UserEmail;
    public GameObject LoginPanel; //, UserPanel;

    private string imageUrl;
    private bool isGoogleSignInInitialized = false;
    
    [SerializeField]
    private UserSession userSession;

    private void Start()
    {
        InitFirebase();
        SceneManager.LoadSceneAsync("ControllerLayer", LoadSceneMode.Additive); // loading save manager
    }

    void InitFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void Login()
    {
        if (!isGoogleSignInInitialized)
        {
            GoogleSignIn.Configuration = new GoogleSignInConfiguration
            {
                RequestIdToken = true,
                WebClientId = GoogleAPI,
                RequestEmail = true
            };
            isGoogleSignInInitialized = true;
        }

        GoogleSignIn.DefaultInstance.SignIn().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogWarning("Google sign-in was canceled.");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("Google sign-in encountered an error: " + task.Exception);
                return;
            }

            GoogleSignInUser googleUser = task.Result;

            Credential credential = GoogleAuthProvider.GetCredential(googleUser.IdToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(authTask =>
            {
                if (authTask.IsCanceled)
                {
                    Debug.LogWarning("Firebase auth was canceled.");
                    return;
                }

                if (authTask.IsFaulted)
                {
                    Debug.LogError("Firebase auth failed: " + authTask.Exception);
                    return;
                }

                user = auth.CurrentUser;
                
                userSession.SetUser(user);
                
                Username.text = user.DisplayName;
                UserEmail.text = user.Email;
                
                // save user session
                UserSession.Instance.SetUser(user);

                LoginPanel.SetActive(false);
                SceneManager.LoadScene("Scenes/Start");
                // UserPanel.SetActive(true);
            });
        });
    }
    // User SignOut From Firebase First Then again Sign IN With Google
    public void SignOut()
    {
        GoogleSignIn.DefaultInstance.SignOut();
        LoginPanel.SetActive(true);
        // UserPanel.SetActive(false);
    }
}
