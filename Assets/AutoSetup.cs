using Modules.SaveData;
using UnityEngine;
using Modules.SaveData.Controllers;
using Modules.SaveData.Services;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoSetup : MonoBehaviour
{

    // private GameObject _game1;
    private GameObject _game2;
    // private GameObject _game3;
    private GameObject _game4;
    // private GameObject _game5;

    [Header("Select parents")]
    public GameObject hScores;
    // public GameObject facts;
    // public GameObject monsters;

    private TMP_Text[] _hScores;
    // private Image[] _facts;
    // private Image[] _monsters;
    private void Start()
    {
        if ( Debug.isDebugBuild )
        {
            SaveManager.Load();
            SceneManager.LoadSceneAsync("ControllerLayer", LoadSceneMode.Additive);
            SceneManager.sceneLoaded +=  OnSceneLoaded;
        }

        if (!Debug.isDebugBuild)
        {
            Init();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "ControllerLayer")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to prevent multiple calls
            Init();
        }
    }

    private void Init()
    {
        //TODO - Change those lol (too much coffee) ~ Hubert
        var laCreatura = CollectiblesController.Instance.Creature;
        var elFacto = CollectiblesController.Instance.Facts;
        
        string[] sceneNames = { "ColorMixingGame", "PaintCatcher", "quiz", "ContainerStacker", "quiz2" };
        var highScores = new int[sceneNames.Length]; // Tablica do przechowywania wyników
        
        Screen.orientation = ScreenOrientation.Portrait;
        // _game1 = GameObject.FindWithTag("Game1");
        _game2 = GameObject.FindWithTag("Game2");
        // _game3 = GameObject.FindWithTag("Game3");
        _game4 = GameObject.FindWithTag("Game4");
        // _game5 = GameObject.FindWithTag("Game5");

        _hScores = hScores.GetComponentsInChildren<TMP_Text>();
        // _facts = facts.GetComponentsInChildren<Image>();
        // _monsters = monsters.GetComponentsInChildren<Image>();

        if (Debug.isDebugBuild)
        {
            Debug.Log(_hScores.Length);
            // Debug.Log(_monsters.Length);
            // Debug.Log(_facts.Length);
        }
        
        // Iteracja przez sceny z użyciem pętli for
        for (int i = 0; i < sceneNames.Length; i++)
        {
            // only two games right now
            if (i == 1 || i == 3)
                highScores[i] = GameStateService.GetHighScoreBySceneName(sceneNames[i]);

        }

        // if (!GameStateService.GetPlayedBySceneName(sceneNames[0]))
        // {
        //     _game1.SetActive(false);
        // }
        if (!GameStateService.GetPlayedBySceneName(sceneNames[1]))
        {
            _game2.SetActive(false);
        }
        // if (!GameStateService.GetPlayedBySceneName(sceneNames[2]))
        // {
        //     _game3.SetActive(false);
        // }
        if (!GameStateService.GetPlayedBySceneName(sceneNames[3]))
        {
            _game4.SetActive(false);
        }
        // if (!GameStateService.GetPlayedBySceneName(sceneNames[4]))
        // {
        //     _game5.SetActive(false);
        // }

        for (var i = 0; i < _hScores.Length; i++)
        {
            var hs = _hScores[i];
            // only two games right now
            if (hs != null && i == 1 || hs != null && i == 3)
            {
                hs.text = highScores[i].ToString();
                Debug.Log($"game_{i} high score: {highScores[i]}");
            }
        }
        // for (var i = 0; i < _monsters.Length; i++)
        // {
        //     var monster = _monsters[i];
        //     if (!laCreatura.IsCollected($"{i+1}")) monster.enabled = false;
        // }
        
        // for (var i = 0; i < _facts.Length; i++)
        // {
        //     var fact = _facts[i];
        //     if  (!elFacto.IsCollected($"{i+1}")) fact.enabled = false;
        // }
    }
    private void OnDestroy()
    {
        if (Debug.isDebugBuild)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
