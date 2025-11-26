using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Modules.PaintCatcher.Scripts {
    public class CartController : MonoBehaviour {
        public float speed = 900f;
        public Color colorChosen = Color.grey;
        public ItemCatcher itemCatcher;
        public TextMeshProUGUI scoreText;
        public SpriteRenderer cart;

        private int _gameScore;
        private Vector3 _target = new Vector3(620, 10, 12);
        private Camera _camera;

        void Start() {
            _camera = Camera.main;
            // Inicjalizacja target w metodzie Start
            // target = new Vector3(0, bucketImage.transform.position.y, bucketImage.transform.position.z);
            // target = new Vector3(-49, -70, 12);
        }


        private void Awake() {
            // Włącz Enhanced Touch
            EnhancedTouchSupport.Enable();
        }

        private void Update() {
            if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0) {
                var touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0];
                if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began ||
                    touch.phase == UnityEngine.InputSystem.TouchPhase.Moved) {
                    if (_camera) {
                        var touchPosition = Camera.main.ScreenToWorldPoint(touch.screenPosition);
                        touchPosition.z = 0;

                        var clampedX = Mathf.Clamp(touchPosition.x, 150, Screen.width - 150);
                        Vector3 targetPosition = new Vector3(clampedX, 10, transform.position.z);

                        // Płynne przesuwanie
                        //transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
                        _target = targetPosition;
                    }

                    // Sprawdź, czy dotyk jest w obszarze, gdzie powinny być krople
                }
            }

            transform.position = Vector3.Lerp(transform.position, _target, speed * Time.deltaTime);
        }


        // public void SetColor(Color color)
        // {
        //     colorChosen = color;
        //     SetTextColor(colorChosen);
        //
        //     cart.color = colorChosen;
        //     paintCatcher.AddScore();
        // }

        public void SetTextColor(Color color) {
            if (scoreText != null) {
                scoreText.color = color; // Zmiana koloru tekstu
            }
        }

        public void AddScore() {
            _gameScore++; // Zwiększ wynik o 1
            itemCatcher.AddScore();
        }


        public void ResetBucket() {
            colorChosen = Color.grey; // Resetuj kolor wiaderka
            SetTextColor(Color.white); // Resetuj kolor tekstu na biały
            transform.position = new Vector3(0, transform.position.y, transform.position.z); // Resetuj pozycję
            _gameScore = 0; // Resetuj wynik
            ScoreManager.ScoreChanged(_gameScore);
            _target = transform.position;
        }
    }
}