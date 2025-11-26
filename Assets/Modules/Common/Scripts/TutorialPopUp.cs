using System;
using Modules.SaveData.Services;
using TMPro;
using UnityEngine;

namespace Modules.Common.Scripts {

public class TutorialPopUp : MonoBehaviour
{
    // public TMP_Text header;
    public TMP_Text content;
    public TutorialDB tutorialDB;
    public GameObject objToDelete;
    private TutorialDataObject _tutorialData;
    public static event Action OnPause;
    public static event Action OnResume;
    
    //TODO - Check if those Actions are needed
    private void OnEnable() {
        OnPause?.Invoke();
        _tutorialData = tutorialDB.GetTutorialForScene(gameObject.scene.name);
        if (GameStateService.GetPlayedBySceneName(gameObject.scene.name)) {
            Destroy(objToDelete);
        };
        // header.text = _tutorialData.sceneNameHeader;
        content.text = $"<size=150%><b>{_tutorialData.sceneNameHeader}</b></size>\n\n"+_tutorialData.content;
    }

    public void OnClose() {
        OnResume?.Invoke();
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
}
