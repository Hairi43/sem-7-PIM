using UnityEngine;
using Modules.SaveData;
using Modules.SaveData.Services;
using Modules.SaveData.Services.Modules.SaveData.Services;
using Modules.SaveData.Controllers;
using UnityEngine.UI;

public class flowers_watering : MonoBehaviour, IShowPopups
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
            if (!CollectiblesController.Instance.Facts.IsCollected("2"))
            {
                CollectiblesController.Instance.Facts.Collect("2");
                SaveManager.Save();
                // Sprite newSprite = Resources.Load("MapMarkers/pin_2_green", typeof(Sprite)) as Sprite;
                // var marker = GameObject.FindWithTag("Pin_2");
                // Image imageComponent = marker.GetComponent<Image>();

                // if (imageComponent != null)
                // {
                //     imageComponent.sprite = newSprite;
                //     Debug.Log("sprite changed");
                // }
            }
            _popupNotification.Open("", "We are not allowed to water the plants in the office. This is part of the agreement we have with the company that takes care of our plants.");
        }
        else 
        {
            Debug.Log("Problem w cube0. Wartość null");
        }
    }
}
