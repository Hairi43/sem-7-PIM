using System.Collections;
using System.Collections.Generic;
using Modules.Common.Scripts.Managers;
using Modules.SaveData;
using Modules.SaveData.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace Modules.Quiz.Scripts {
    public class Quiz : MonoBehaviour
    {
        [Header("UI References")]
        public TMP_Text questionText;
        public Button[] answerButtons;
        public Image[] statusImage;
        
        [Header("Resources")]
        public Sprite[] statusSprites;
        [SerializeField]
        public QuizDatabase questionDb;

        private char _userAnswer = 'u';
        private char _correctAnswer = 'z';
        private bool _inputLocked;
        private string _sceneName;
        private int _score;
        private int _highScore;
        private List<Question> _questions;

        public AnimationManager rollerAnim;


        private void Start() {
            _questions = questionDb.questions;
            _score = 0;
            _highScore = 0;
            if (Debug.isDebugBuild) SaveManager.Load();
            if (GameStateService.GetPlayedBySceneName(_sceneName)) {
                Time.timeScale = 1;
            }

            _sceneName = SceneManager.GetActiveScene().name;
            GameStateService.CreateGameStateBySceneName(_sceneName);
            if(Debug.isDebugBuild) SaveManager.Save();
            if (!QuizState.questionLoaded)
            {
                LoadQuestion();
                QuizState.questionLoaded = true;
            }
            
            rollerAnim.OnAnimationReturn = HandlePostAnswer;
            AssignButtonCallbacks();
        }
        private void Awake() {
            QuizState.usedQuestions.Clear();
            QuizState.questionLoaded = false;
        }

        private void AssignButtonCallbacks()
        {
            answerButtons[0].onClick.AddListener(() => OnAnswer('a'));
            answerButtons[1].onClick.AddListener(() => OnAnswer('b'));
            answerButtons[2].onClick.AddListener(() => OnAnswer('c'));
            answerButtons[3].onClick.AddListener(() => OnAnswer('d'));
        }


        private void LoadQuestion()
        {
            if (QuizState.usedQuestions.Count >= _questions.Count)
            {
                QuizState.usedQuestions.Clear();
                return;
            }

            int index;
            do { index = Random.Range(0, _questions.Count); }
            while (QuizState.usedQuestions.Contains(index));

            var question = _questions[index];

            questionText.text = question.question;
            for (var i = 0; i < answerButtons.Length; i++)
            {
                answerButtons[i].GetComponentInChildren<TMP_Text>().text = question.answers[i];
                statusImage[i].color = Color.white;
            }

            _correctAnswer = question.correctAnswer;
            QuizState.usedQuestions.Add(index);
        }

        public void OnAnswer(char selected)
        {
            if (_inputLocked) return;

            _userAnswer = selected;
            _inputLocked = true;
            var index = selected - 'a';
            var correctIndex = _correctAnswer - 'a';
            
            if (_userAnswer == _correctAnswer) {
                _score++;
                _highScore = Mathf.Max(_score, _highScore);
                PlayFade(index, Color.green,Status.Correct);
            }
            else
            {
                // _score = 0;
                PlayFade(index, Color.red, Status.Wrong);
                PlayFade(correctIndex, Color.green, Status.Correct);
                // TurnButtonColor(_correctAnswer, Color.green);
            }

            ScoreManager.ScoreChanged(_score);
            rollerAnim.PlayAnimation("rollingAnim");
        }

        private void HandlePostAnswer()
        {
            if (QuizState.usedQuestions.Count < 3)
            {
                QuizState.questionLoaded = false;
                ResetGame();
            }
            else
            {
                GameDone(_score >= 2);
                QuizState.usedQuestions.Clear();
                QuizState.questionLoaded = false;
            }
        }
        private void PlayFade(int index, Color color, Status state)
        {
            if (index < 0 || index >= statusImage.Length) return;

            var image = statusImage[index];
            image.color = color;
            image.sprite = statusSprites[(int)state];
            image.gameObject.SetActive(true);

            StartCoroutine(FadeSequence(image));
        }

        private IEnumerator FadeSequence(Image img)
        {
            yield return Fade(img, 0f, 1f, 0.2f);        // fade in
            yield return new WaitForSeconds(0.1f);
            yield return Fade(img, 1f, 0f, 0.2f);        // fade out
            img.gameObject.SetActive(false);
        }

        private IEnumerator Fade(Image img, float from, float to, float duration)
        {
            var time = 0f;
            var color = img.color;

            while (time < duration)
            {
                time += Time.deltaTime;
                var alpha = Mathf.Lerp(from, to, time / duration);
                img.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }

            img.color = new Color(color.r, color.g, color.b, to);
        }

        private void GameDone(bool isWon) {
            
            _highScore = Mathf.Max(_highScore, _score);
            EndOfGameManager.GameFinished(isWon, _highScore);
            
        }

        private void ResetGame() {
            {
                _inputLocked = false;
                _userAnswer = 'u';
                _correctAnswer = 'z';
    
                foreach (var img in statusImage)
                {
                    img.color = Color.white;
                    img.sprite = null;
                    img.gameObject.SetActive(false);
                }
                LoadQuestion();
            }
        }
    }
}