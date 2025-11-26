using Modules.Common.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfGameScreen : MonoBehaviour {
    public GameObject panel;
    public Sprite[] endOfGameBanners;
    public Image banner;

    private void OnEnable()
    {
        EndOfGameManager.OnGameFinished += ShowScreen;
    }

    private void OnDisable()
    {
        EndOfGameManager.OnGameFinished -= ShowScreen;
    }

    private void ShowScreen (bool isWon, int score)
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        banner.sprite = isWon ? endOfGameBanners[1] : endOfGameBanners[0];
        if (score == 0) return;
        ScoreManager.ScoreChanged(score);
    }

    public void OnReplayClick() {
        var scene = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
    public void OnHomeClick()
    {
        SceneManager.LoadScene("MAP");
    }
    public void OnReturnClick() {
        if (PlayerPrefs.HasKey("lastScene"))
        {
            var lastScene = PlayerPrefs.GetString("lastScene");
            SceneManager.LoadScene(lastScene);
            Time.timeScale = 1;
        }
    }
}
