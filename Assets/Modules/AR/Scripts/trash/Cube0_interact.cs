using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Cube0_interact : MonoBehaviour
{
    public Camera camera;
    public TMP_Text output;
    string btnName;

    PopupNotification _popupNotification;
    GameObject popupController;

    // ArrayCastManager arrayMan;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // arrayMan = GetComponent<ArrayCastManager>();
        popupController = GameObject.Find("PopUpController");
        _popupNotification = popupController.GetComponent<PopupNotification>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        // if (Input.touchCount > 0)
        // {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            output.text = "clicked!";
            if (Physics.Raycast(ray, out hit))
            {
                // output.text = "got hit";
                // btnName = hit.transform.name;
                // switch (btnName)
                // {
                //     case "Cube0":
                //         output.text = "hit cube 0";
                //         break;
                //     case "Cube1":
                //         output.text = "hit cube 1";
                //         break;
                //     case "Cube2":
                //         output.text = "hit cube 2";
                //         break;
                //     case "Cube3":
                //         output.text = "hit cube 3";
                //         break;
                //     default:
                //         output.text = "";
                //         break;
                // }
            }
        }
    }
}
