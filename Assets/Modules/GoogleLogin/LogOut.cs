using Google;
using Modules.SaveData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.GoogleLogin
{
    public class LogOut : MonoBehaviour
    {
        // User SignOut From Firebase First Then again Sign IN With Google
        public void SignOut()
        {
            SaveManager.ClearData();
            DestroyAllDDOLObjects();
            GoogleSignIn.DefaultInstance.SignOut();
            SceneManager.LoadScene("LoginScreen");
        }
        
        public static void DestroyAllDDOLObjects()
        {
            Scene ddol = SceneManager.GetSceneByName("DontDestroyOnLoad");

            if (!ddol.isLoaded)
            {
                Debug.LogWarning("DDOL scene not loaded!");
                return;
            }

            foreach (GameObject obj in ddol.GetRootGameObjects())
            {
                if (obj.name != "Firebase Services")
                    Destroy(obj);
            }

            Debug.Log("All DontDestroyOnLoad objects destroyed.");
        }
    }
}