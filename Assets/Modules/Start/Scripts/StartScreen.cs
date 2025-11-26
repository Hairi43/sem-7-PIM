using Modules.SaveData;
using Modules.SaveData.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Modules.Start.Scripts
{
    public class StartScreen : MonoBehaviour
    {
        private void Start() {
            // SceneManager.LoadSceneAsync("ControllerLayer", LoadSceneMode.Additive);
            Screen.orientation = ScreenOrientation.Portrait;
            //Start the game load saved data from json
            SaveManager.Load();
        }

        public void OnButtonClick()
        {
            GameStateService.Initialize();
            SceneManager.LoadScene("info");
            // Debug.LogError("Entering LoginScreen scene");
            // SceneManager.LoadScene("LoginScreen"); // issue - null or something
        }
    }
}
