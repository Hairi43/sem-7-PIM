using System.Collections;
using System.Linq;
using Modules.SaveData;
using Modules.SaveData.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExpandableMenu : MonoBehaviour {

    [Header("Spacing między guzikami (UI units)")]
    public Vector2 spacing;

    [Header("Czas animacji (s)")] 
    public float duration;
    
    public GameObject modal;

    private Button _menuButton;
    private ExpandableMenuItem[] _menuItems;
    private bool _isExpanded = false;
    private Vector2 _menuButtonPosition;

    private int _itemCount;

    void Start() {
        
        if (Debug.isDebugBuild) {
            SaveManager.Load();
        }
        
        var scene = SceneManager.GetActiveScene().name;
        if (!GameStateService.GetPlayedBySceneName(scene)) {
            if (modal == null) {
                modal = Instantiate(modal);
                Debug.Log("Modal was null");
            }
            modal.SetActive(true);
        }
        
        _itemCount = transform.childCount - 1;

        _menuItems = Enumerable.Range(1, _itemCount)
            .Select(index => transform.GetChild(index).GetComponent<ExpandableMenuItem>())
            .ToArray();

        _menuButton = transform.GetChild(0).GetComponent<Button>();
        _menuButton.transform.SetAsLastSibling();
        _menuButton.onClick.AddListener(OnMenuClick);

        _menuButtonPosition = _menuButton.GetComponent<RectTransform>().anchoredPosition;

        foreach (var button in _menuItems) {
            button.offset.anchoredPosition = _menuButtonPosition;
        }
    }
    
    private IEnumerator Move(RectTransform target, Vector2 targetPosition) {

        var startPosition = target.anchoredPosition;
        
        var time = 0f;
        while (time < duration) {
            
            time += Time.deltaTime;
            var step = time / duration;
            var angle = Mathf.Lerp(0f, _isExpanded ? -360f : 360f, step);

            target.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, step);
            target.localEulerAngles = new Vector3(0f, 0f, angle);
            
            yield return null;
        }

        target.anchoredPosition = targetPosition;
        target.localEulerAngles = Vector3.zero;
    }

    private void OnMenuClick() {
        _isExpanded = !_isExpanded;

        if (_isExpanded) {
            var i = 0;
            foreach (var button in _menuItems) {
                var targetPosition = _menuButtonPosition + spacing * (i + 1);
                StartCoroutine(Move(button.offset, targetPosition));
                i++;
            }
        } else {
            foreach (var button in _menuItems) {
                StartCoroutine(Move(button.offset, _menuButtonPosition));
            }
        }
    }

    public void OnSettingsClick()
    {
        
    }

    public void OnTutorialClick()
    {
        modal.SetActive(true);
    }
    public void OnRestartClick()
    {
        var scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
    public void OnReturnClick() {
        if (PlayerPrefs.HasKey("lastScene"))
        {
            var lastScene = PlayerPrefs.GetString("lastScene");
            SceneManager.LoadScene(lastScene);
        }
    }
}