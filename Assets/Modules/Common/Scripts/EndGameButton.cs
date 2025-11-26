using System;
using Modules.Common.Scripts.Managers;
using UnityEngine;

namespace Modules.Common.Scripts
{
    public class EndGameButton : MonoBehaviour
    {
        public GameObject container;
        public CanvasGroup canvasGroup;

        public void OnEnable()
        {
            EndOfGameManager.OnEndGameAvailable += OnEndGamePossible;
        }

        public void OnDisable()
        {
            EndOfGameManager.OnEndGameAvailable -= OnEndGamePossible;
        }
        
        private void OnEndGamePossible()
        {
            container.SetActive(true);
            Invoke(nameof(HideText), 3f);    
        }

        private void HideText()
        {
            canvasGroup.alpha = 0;
        }

        public void OnEndGameButton()
        {
            Debug.Log("EndGameButton clicked");
            var result = ScoreManager.GetScore();
            EndOfGameManager.GameFinished(true,result);
            container.SetActive(false);
        }
        
    }
}
