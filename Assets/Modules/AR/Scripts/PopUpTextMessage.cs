// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public static class PopUpTextMessage : MonoBehaviour {

// 	public GameObject uiText;

//     // [SerializeField] 
//     // private TMP_Text mainText;

// 	// Use this for initialization
// 	void Start () {

// 	}
	
// 	// Update is called once per frame
// 	void Update () {
    
//     }

// 	public static void OpenText(){
// 		uiText.SetActive (!uiText.activeSelf);

// 		if (uiText.activeSelf) {
// 			Time.timeScale = 0f;
// 		} 
// 	}

// 	public static void CloseText(){
// 		uiText.SetActive (!uiText.activeSelf);
// 		if (!uiText.activeSelf) {
// 			Time.timeScale = 1f;
// 		} 
// 	}
// }