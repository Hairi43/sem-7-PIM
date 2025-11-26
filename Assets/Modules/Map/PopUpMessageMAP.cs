using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupMessageMAP : MonoBehaviour {

	public GameObject ui;
	public RawImage rawImage;

    // [SerializeField] 
    // private TMP_Text mainText; // not used here

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
    
    }

	public void OpenMAP(string inventoryStuffName, string message){
		ui.SetActive (!ui.activeSelf);

		if (ui.activeSelf) {
			if(!string.IsNullOrEmpty(inventoryStuffName)){
				var texture = TakeInvenotryCollecitionMAP(inventoryStuffName);
				// RawImage rawImage = ui.gameObject.GetComponentInChildren<RawImage>();
				if (texture == null)
				{
					Debug.Log("null texture popupmessage");
				}
				rawImage.texture = texture;
			}
			// if (!string.IsNullOrEmpty (message)) {
			// 	// TMP_Text textObject = ui.GetComponentInChildren<TMP_Text> ();
			// 	mainText.text = message;
			// }
            // if (!string.IsNullOrEmpty (messageTop)) {
			// 	Text textTopObject = ui.gameObject.GetComponentInChildren<TextTop> ();
			// 	textTopObject.text = messageTop;
			// }
			Time.timeScale = 0f;
		} 
	}

	public void CloseMAP(){
		ui.SetActive (!ui.activeSelf);
		if (!ui.activeSelf) {
			Time.timeScale = 1f;
		} 
	}

	

    //You need to have Folder Resources/InvenotryItems
	public Texture TakeInvenotryCollecitionMAP(string LoadCollectionsToInventory)
	{
		Debug.Log("takeInverntoryCollectionMAP: "+LoadCollectionsToInventory);
		Texture loadedGO = Resources.Load("MAPPopUpImages/"+LoadCollectionsToInventory, typeof(Texture)) as Texture;
		return loadedGO;
	}
}