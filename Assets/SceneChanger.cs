using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    private string _lastScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() {
        _lastScene = SceneManager.GetActiveScene().name;
    }

    public void GOTO_INFO(){
        SceneManager.LoadScene("info");
    }

    public void GOTO_MAP(){
        SceneManager.LoadScene("MAP");
    }

    
    public void GOTO_ACHIGMENTS(){
        SceneManager.LoadScene("achigments");
    }

    public void GOTO_SCANNER(){
        SceneManager.LoadScene("ARScene");
    }
    public void GOTO_MINIGAME1()
    {
        PlayerPrefs.SetString("lastScene", _lastScene);
        PlayerPrefs.Save();
        SceneManager.LoadScene("ColorMixingGame");
    }
    public void GOTO_MINIGAME2()
    {
        PlayerPrefs.SetString("lastScene", _lastScene);
        PlayerPrefs.Save();
        SceneManager.LoadScene("PaintCatcher");
    }
    public void GOTO_MINIGAME3()
    {
        PlayerPrefs.SetString("lastScene", _lastScene);
        PlayerPrefs.Save();
        SceneManager.LoadScene("quiz");
    }
    public void GOTO_MINIGAME4(){
        PlayerPrefs.SetString("lastScene", _lastScene);
        PlayerPrefs.Save();
        SceneManager.LoadScene("ContainerStacker");
    }
    public void GOTO_MINIGAME5(){
        PlayerPrefs.SetString("lastScene", _lastScene);
        PlayerPrefs.Save();
        SceneManager.LoadScene("quiz2");
    }
    
}
