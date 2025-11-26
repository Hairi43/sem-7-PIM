using TMPro;
using UnityEngine;

namespace Modules.ColorMixing.Scripts {
    public class HighScoreSetter : MonoBehaviour
    {
        public TMP_Text scoreText;
        public GameObject highScoreContainer;

        private void OnEnable()
        {
            ScoreManager.OnHighScoreChanged += UpdateScore;
        }

        private void OnDisable()
        {
            ScoreManager.OnHighScoreChanged -= UpdateScore;
        }

        private void UpdateScore(int score) {
            Debug.Log("Enable HS");
        
            if (score > 0) highScoreContainer.SetActive(true);
            scoreText.text = $"{score}";
        }
    }
}
