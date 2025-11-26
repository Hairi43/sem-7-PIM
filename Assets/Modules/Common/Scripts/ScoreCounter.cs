using System;
using TMPro;
using UnityEngine;

public class ScoreSetter : MonoBehaviour {
    
    public TMP_Text scoreText;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScore;
        ScoreManager.OnGetScore += GetScore;
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScore;
        ScoreManager.OnGetScore -= GetScore;
    }

    private void UpdateScore(int score)
    {
        scoreText.text = $"{score}";
    }

    private int GetScore() => Convert.ToInt32(scoreText.text);

}
