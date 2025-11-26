using System.Collections.Generic;
using System.Linq;
using Modules.Common.Scripts.Managers;
using Modules.SaveData;
using Modules.SaveData.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Modules.ColorMixing.Scripts {
    public class ColorMixing : MonoBehaviour
    {
        [Header("UI Elements")]
        public Image targetColorImage;
        public Image resultColorImage;
        public Button[] colorButtons;
        public Button checkButton;
        public TMP_Text messageText;
        [Header("Highlights")]
        public Image[] highlight;
        [Header("Ilość rund")] 
        public int roundLimit;
    
        public AnimationManager brushAnimManager;
        public StatusFXManager statusFXManager;

        private string _sceneName = "";
        private Color[] GameColors {get; set;}    
        private readonly List<Color> _selectedColors = new (2);
        private bool _isCheckReady;
        private int _firstPressedIndex;
        private int _secondPressedIndex;
        private int _score;
        private int _highScore;
        private int _round;


        private readonly Color[] _colors = new Color[] {
            Color.red, Color.blue, Color.green, Color.yellow,
            Color.cyan, Color.gray, Color.magenta, Color.white
        };

        private void Start() {
            
            //For PC testing
            if (Debug.isDebugBuild) {
                SaveManager.Load();
            }

            _sceneName = SceneManager.GetActiveScene().name;
            //Make sure game data exists at first
            GameStateService.CreateGameStateBySceneName(_sceneName);
            if (GameStateService.GetPlayedBySceneName(_sceneName)) {
                Time.timeScale = 1;
            }
            
            Screen.orientation = ScreenOrientation.Portrait;
            brushAnimManager.OnAnimationEnd = OnAnimationDone;
            Init();   
            checkButton.onClick.RemoveAllListeners();
            checkButton.onClick.AddListener(OnCheckResult);
        }

        private void Init() {
            _round++;
            _selectedColors.Clear();
            _firstPressedIndex = -1;
            _secondPressedIndex = -1;
            GameColors = GetColors(_colors, 4);
            for (var i = 0; i < colorButtons.Length; i++)
            {
                var index = i;
                colorButtons[i].GetComponent<Image>().color = GameColors[i];
                colorButtons[i].onClick.RemoveAllListeners();
                highlight[i].GetComponent<Image>().enabled = false;
                colorButtons[i].onClick.AddListener(() => OnSelectColor(index));
            }

            var target = GetColors(GameColors, 2);
            targetColorImage.color = Color.Lerp(target[0], target[1], 0.5f);
            resultColorImage.color = Color.white;
            messageText.text = "";
        }

        private static Color[] GetColors(IEnumerable<Color> colors, int selectedCount)
        {
            var random = new System.Random();
            var colorList = colors.ToList();

        
            for (var i = 0; i < selectedCount; i++)
            {
                var j = random.Next(i, colorList.Count);
                (colorList[i], colorList[j]) = (colorList[j], colorList[i]);
            }

            return colorList.Take(selectedCount).ToArray();
        }

        private void OnSelectColor(int index)
        {
            highlight[index].GetComponent<Image>().enabled = true;
        
            if(index == _firstPressedIndex || index == _secondPressedIndex ) return;

            switch (_selectedColors.Count)
            {
                case 0:
                    _selectedColors.Add(GameColors[index]);
                    _firstPressedIndex = index;
                    resultColorImage.color = _selectedColors[0];
                    break;
                case 1:
                    _selectedColors.Add(GameColors[index]);
                    _secondPressedIndex = index;
                    break;
                default:
                    highlight[_firstPressedIndex].GetComponent<Image>().enabled = false;
                    _firstPressedIndex = _secondPressedIndex;
                    _secondPressedIndex = index;

                    _selectedColors[0] = _selectedColors[1];
                    _selectedColors[1] = GameColors[index];
                    break;
            }

            if (_selectedColors.Count != 2) return;
        
            var result = Color.Lerp(_selectedColors[0], _selectedColors[1], 0.5f);
            resultColorImage.color = result;
            _isCheckReady = true;
        }

        private void OnCheckResult() {
            if (!_isCheckReady)
            {
                messageText.text = "Select two colors!";
                statusFXManager.PlayFade(Color.yellow, Status.Error);
                return;
            }
            _isCheckReady = false;
            brushAnimManager.PlayAnimation("Brush_mix");
        }

        private void OnAnimationDone()
        {
            if (targetColorImage.color == resultColorImage.color)
            {
                // messageText.text = "Udało się!";
                statusFXManager.PlayFade(Color.green, Status.Correct);
            
                _score++;
                if (_score >= _highScore) _highScore = _score;
            }
            else
            {
                resultColorImage.color = Color.white;
                // messageText.text = "Spróbuj jeszcze raz!";
                statusFXManager.PlayFade(Color.red, Status.Wrong);
                ScoreManager.HighScoreChanged(_highScore);
                _score = 0;
            }

            ScoreManager.ScoreChanged(_score);
            if (_round >= roundLimit) {
                
                GameDone(_score >= 3);
            }
            else {
                Invoke(nameof(Init), 1f);
            }
        }

        private void GameDone(bool isWon)
        {
            _highScore = Mathf.Max(_score, _highScore);
            EndOfGameManager.GameFinished(isWon, _highScore);
        }
    }
}