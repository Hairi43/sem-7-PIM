using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupNotification : MonoBehaviour {

	public GameObject ui;
	public GameObject uiText;
	public GameObject uiOnce;
	public GameObject uiARInfo;

    [SerializeField] 
    private TMP_Text mainText;

	public static PopupNotification Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}

	// Use this for initialization
	void Start () {
		// if (Instance == null)
        // {
        //     Instance = this;
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }
	}
	
	// Update is called once per frame
	void Update () {
    
    }

	public void Open(string inventoryStuffName, string message){
		if (ui == null)
		{
			Debug.Log("uiOnce is null");
			ui = GameObject.Find("PopUpMessage");
			// uiOnce = Instantiate(uiOnce);
			// return;
		}

		ui.SetActive (!ui.activeSelf);

		if (ui.activeSelf) {
			if(!string.IsNullOrEmpty(inventoryStuffName)){
				var texture = TakeInvenotryCollecition (inventoryStuffName);
				RawImage rawImage = ui.gameObject.GetComponentInChildren<RawImage>();
				rawImage.texture = texture;
			}
			if (!string.IsNullOrEmpty (message)) {
				// TMP_Text textObject = ui.GetComponentInChildren<TMP_Text> ();
				mainText.text = message;
			}
            // if (!string.IsNullOrEmpty (messageTop)) {
			// 	Text textTopObject = ui.gameObject.GetComponentInChildren<TextTop> ();
			// 	textTopObject.text = messageTop;
			// }
			Time.timeScale = 0f;
		} 
	}

	public void OpenText(){
		if (uiText == null)
		{
			Debug.Log("uiOnce is null");
			uiText = GameObject.Find("PopUpTextMessage");
		}

		uiText.SetActive (!uiText.activeSelf);

		if (uiText.activeSelf) {
			Time.timeScale = 0f;
		} 
	}


	public void Close(){
		if (ui == null)
		{
			Debug.Log("uiOnce is null");
			ui = GameObject.Find("PopUpMessage");
		}

		ui.SetActive (!ui.activeSelf);
		if (!ui.activeSelf) {
			Time.timeScale = 1f;
		} 
	}

	public void CloseText(){
		if (uiText == null)
		{
			Debug.Log("uiOnce is null");
			uiText = GameObject.Find("PopUpTextMessage");
		}

		uiText.SetActive (!uiText.activeSelf);
		if (!uiText.activeSelf) {
			Time.timeScale = 1f;
		} 
	}

	public void OpenOnce(){
		Debug.Log($"this name: {this.name}");

		// if (ui == null)
		// {
		// 	Debug.Log("ui is null");
		// 	ui = GameObject.Find("PopUpMessage");
		// 	// ui = Instantiate(ui);
		// 	// return;
		// }
		// if (uiText == null)
		// {
		// 	Debug.Log("uiText is null");
		// 	uiText = GameObject.Find("PopUpTextMessage");
		// 	// uiText = Instantiate(uiText);
		// 	// return;
		// }
		if (uiOnce == null)
		{
			Debug.Log("uiOnce is null");
			uiOnce = GameObject.Find("PopUpTutorial");
			// uiOnce = Instantiate(uiOnce);
			// return;
		}

		uiOnce.SetActive (!uiOnce.activeSelf);

		if (uiOnce.activeSelf) {
			Debug.Log("activated uiOnce. We happy");
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

	public void CloseOnce(){
		if (uiOnce == null)
		{
			Debug.Log("uiOnce is null");
			uiOnce = GameObject.Find("PopUpTextMessage");
			// uiOnce = Instantiate(uiOnce);
			// return;
		}

		uiOnce.SetActive (!uiOnce.activeSelf);
		if (!uiOnce.activeSelf) {
			Time.timeScale = 1f;
		} 
	}

	public void OpenARInfo(){
		Debug.Log($"this name: {this.name}");
		if (uiARInfo == null)
		{
			Debug.Log("uiARInfo is null");
			uiARInfo = GameObject.Find("PopUpARInfo");
		}

		uiARInfo.SetActive (!uiARInfo.activeSelf);

		if (uiARInfo.activeSelf) {
			Debug.Log("activated uiARInfo. We happy");
			
			Time.timeScale = 0f;
		}
		
	}

	public void CloseARInfo(){
		if (uiARInfo == null)
		{
			Debug.Log("uiARInfo is null");
			uiARInfo = GameObject.Find("PopUpARInfo");
		}

		uiARInfo.SetActive (!uiARInfo.activeSelf);
		if (!uiARInfo.activeSelf) {
			Time.timeScale = 1f;
		} 
	}

    //You need to have Folder Resources/InvenotryItems
	public Texture TakeInvenotryCollecition(string LoadCollectionsToInventory)
	{
		Texture loadedGO = Resources.Load("PopUpImages/"+LoadCollectionsToInventory, typeof(Texture)) as Texture;
		return loadedGO;
	}
}