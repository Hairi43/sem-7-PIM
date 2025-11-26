using UnityEngine;

public class monster_3 : MonoBehaviour, IShowPopups
{

    [SerializeField]
    PopupMessageMAP popupMessage;
    [SerializeField]
    GameObject popupController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // popupController = GameObject.Find("PopUpControllerMAP");
		// popupMessage = popupController.GetComponent<PopupMessage> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowPopUp()
    {
        if (popupController != null && popupMessage != null)
        {
            popupMessage.OpenMAP("play_room", "neony w kuchni zostały zaprojektowane przez nasze koleżanki, Martynę Kubal i Marysię Sierpińską?");
        }
        else 
        {
            Debug.Log("Problem w cube0. Wartość null");
        }
    }
}
