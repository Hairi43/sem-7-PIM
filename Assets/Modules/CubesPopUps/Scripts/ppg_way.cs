using UnityEngine;
using Modules.SaveData;
using Modules.SaveData.Services;
using Modules.SaveData.Services.Modules.SaveData.Services;
using Modules.SaveData.Controllers;
using UnityEngine.UI;

public class ppg_way : MonoBehaviour, IShowPopups
{

    PopupNotification _popupNotification;
    GameObject popupController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        popupController = GameObject.Find("PopUpController");
		_popupNotification = popupController.GetComponent<PopupNotification> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopUp()
    {
        if (popupController != null && _popupNotification != null)
        {
            if (!CollectiblesController.Instance.Facts.IsCollected("8"))
            {
                CollectiblesController.Instance.Facts.Collect("8");
                SaveManager.Save();
                // Sprite newSprite = Resources.Load("MapMarkers/pin_8_green", typeof(Sprite)) as Sprite;
                // var marker = GameObject.FindWithTag("Pin_8");
                // Image imageComponent = marker.GetComponent<Image>();

                // if (imageComponent != null)
                // {
                //     imageComponent.sprite = newSprite;
                //     Debug.Log("sprite changed");
                // }
            }
            _popupNotification.Open("", "How does the PPG Way help us at work? It provides us with a set of shared values that remain consistent regardless of cultural differences.");
        }
        else 
        {
            Debug.Log("Problem w cube0. Wartość null");
        }
    }
}
