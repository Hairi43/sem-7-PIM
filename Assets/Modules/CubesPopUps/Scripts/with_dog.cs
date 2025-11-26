using UnityEngine;
using Modules.SaveData;
using Modules.SaveData.Services;
using Modules.SaveData.Services.Modules.SaveData.Services;
using Modules.SaveData.Controllers;
using UnityEngine.UI;

public class with_dog : MonoBehaviour, IShowPopups
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
            if (!CollectiblesController.Instance.Facts.IsCollected("10"))
            {
                CollectiblesController.Instance.Facts.Collect("10");
                SaveManager.Save();
                Debug.Log("[with_dog] collected 10");
                // Sprite newSprite = Resources.Load("MapMarkers/pin_10_green", typeof(Sprite)) as Sprite;
                // var marker = GameObject.FindWithTag("Pin_10");
                // Image imageComponent = marker.GetComponent<Image>();

                // if (imageComponent != null)
                // {
                //     imageComponent.sprite = newSprite;
                //     Debug.Log("sprite changed");
                // }
            }
            _popupNotification.Open("", "You can bring your dog to the office. During your dog's stay, you are fully responsible for them, and in case of any unexpected issues, you can take refuge in the Myspace room.");
        }
        else 
        {
            Debug.Log("Problem w cube0. Wartość null");
        }
    }
}
