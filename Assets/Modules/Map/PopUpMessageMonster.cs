using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Modules.SaveData.Services.Modules.SaveData.Services;
using Modules.SaveData;
using Modules.SaveData.Services;
using Modules.SaveData.Controllers;

public class PopupMessageMonster : MonoBehaviour {

	public GameObject ui;
	public GameObject uiFacts;
	public RawImage rawImage; // do obiektu ui

    [SerializeField] 
    private TMP_Text mainText;

	[SerializeField] 
    private TMP_Text factsText;

	// Use this for initialization
	void Start () {
        CreatureService _creatureService = new CreatureService();

		if (ui == null)
		{
			Debug.Log("uiOnce is null");
			ui = GameObject.Find("PopUpMessageMonster");
		}

		if (uiFacts == null)
		{
			Debug.Log("uiOnce is null");
			uiFacts = GameObject.Find("PopUpFacts");
		}

        string lastMonster = PlayerPrefs.GetString("lastMonster", "");
        if (!string.IsNullOrEmpty(lastMonster) && lastMonster != "null")
        {
			// monster cellecting is in EndOfGameNamager.ProcessCollection()
            if (_creatureService.IsCollected(lastMonster))
            {
				if (lastMonster == "1")
				{
					// OpenMonsters(lastMonster, "Odkryłeś Funky Frog!");
                	// _creatureService.Collect(lastMonster);
				}
				else if (lastMonster == "2")
				{
					// OpenMonsters(lastMonster, "Odkryłeś Purple Basil!");
                	// _creatureService.Collect(lastMonster);
				}
				else if (lastMonster == "3")
				{
					// OpenMonsters(lastMonster, "Odkryłeś Cranapple!");
                	// _creatureService.Collect(lastMonster);
				}
				else if (lastMonster == "4")
				{
					// OpenMonsters(lastMonster, "Odkryłeś Blue Calico!");
                	// _creatureService.Collect(lastMonster);
				}
				else if (lastMonster == "5")
				{
					// OpenMonsters(lastMonster, "Odkryłeś Gold Buff!");
                	// _creatureService.Collect(lastMonster);
				}   
            }
            PlayerPrefs.SetString("lastMonster", "null");
			PlayerPrefs.Save();
			SaveManager.Save();
        }


	}
	
	// Update is called once per frame
	void Update () {
    
    }

	public void OpenMonsters(string inventoryStuffName, string message){
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

	public void CloseMonsters(){
		ui.SetActive (!ui.activeSelf);
		if (!ui.activeSelf) {
			Time.timeScale = 1f;
		} 
	}

	public void OpenMonster1(){
		ui.SetActive (!ui.activeSelf);

		if (ui.activeSelf) {
			var texture = TakeInvenotryCollecitionMAP("1");
			if (texture == null)
			{
				Debug.Log("null texture popupmessage");
			}
			rawImage.texture = texture;
			mainText.text = "Odkryłeś Funky Frog!";
			Time.timeScale = 0f;
		} 
	}

	public void OpenMonster2(){
		ui.SetActive (!ui.activeSelf);

		if (ui.activeSelf) {
			var texture = TakeInvenotryCollecitionMAP("2");
			if (texture == null)
			{
				Debug.Log("null texture popupmessage");
			}
			rawImage.texture = texture;
			mainText.text = "Odkryłeś Purple Basil!";
			Time.timeScale = 0f;
		} 
	}

		public void OpenMonster3(){
		ui.SetActive (!ui.activeSelf);

		if (ui.activeSelf) {
			var texture = TakeInvenotryCollecitionMAP("3");
			if (texture == null)
			{
				Debug.Log("null texture popupmessage");
			}
			rawImage.texture = texture;
			mainText.text = "Odkryłeś Cranapple!";
			Time.timeScale = 0f;
		} 
	}

		public void OpenMonster4(){
		ui.SetActive (!ui.activeSelf);

		if (ui.activeSelf) {
			var texture = TakeInvenotryCollecitionMAP("4");
			if (texture == null)
			{
				Debug.Log("null texture popupmessage");
			}
			rawImage.texture = texture;
			mainText.text = "Odkryłeś Blue Calico!";
			Time.timeScale = 0f;
		} 
	}

	public void OpenMonster5(){
		ui.SetActive (!ui.activeSelf);

		if (ui.activeSelf) {
			var texture = TakeInvenotryCollecitionMAP("5");
			if (texture == null)
			{
				Debug.Log("null texture popupmessage");
			}
			rawImage.texture = texture;
			mainText.text = "Odkryłeś Gold Buff!";
			Time.timeScale = 0f;
		} 
	}

    //You need to have Folder Resources/InvenotryItems
	public Texture TakeInvenotryCollecitionMAP(string LoadCollectionsToInventory)
	{
		Debug.Log("takeInverntoryCollectionMAP: "+LoadCollectionsToInventory);
		Texture loadedGO = Resources.Load("Monsters/"+LoadCollectionsToInventory, typeof(Texture)) as Texture;
		return loadedGO;
	}


	//
	//	Facts' popups
	//

	public void OpenFact1(){
		uiFacts.SetActive (!uiFacts.activeSelf);

		if (uiFacts.activeSelf) {
			factsText.text = "We have various coffee brewing methods available in the kitchen: a drip coffee maker, Chemex, AeroPress, and pour-over (drip).";
			Time.timeScale = 0f;
		} 
	}

	public void OpenFact2(){
		uiFacts.SetActive (!uiFacts.activeSelf);

		if (uiFacts.activeSelf) {
			factsText.text = "We are not allowed to water the plants in the office. This is part of the agreement we have with the company that takes care of our plants.";
			Time.timeScale = 0f;
		} 
	}

	public void OpenFact3(){
		uiFacts.SetActive (!uiFacts.activeSelf);

		if (uiFacts.activeSelf) {
			factsText.text = "As part of the IDEA program, you can propose an improvement to any process and receive a reward for it. For more information, please contact Małgorzata Pinkas.";
			Time.timeScale = 0f;
		} 
	}

	public void OpenFact4(){
		uiFacts.SetActive (!uiFacts.activeSelf);

		if (uiFacts.activeSelf) {
			factsText.text = "We have a library in our office. You can borrow any book or add something to our collection.";
			Time.timeScale = 0f;
		} 
	}

	public void OpenFact5(){
		uiFacts.SetActive (!uiFacts.activeSelf);

		if (uiFacts.activeSelf) {
			factsText.text = "We have a massage chair in the office. It’s located in room number 19.";
			Time.timeScale = 0f;
		} 
	}

	public void OpenFact6(){
		uiFacts.SetActive (!uiFacts.activeSelf);

		if (uiFacts.activeSelf) {
			factsText.text = "The murals in our hallway were designed by our colleague Kamila Kiełbusa?";
			Time.timeScale = 0f;
		} 
	}

	public void OpenFact7(){
		uiFacts.SetActive (!uiFacts.activeSelf);

		if (uiFacts.activeSelf) {
			factsText.text = "The neon signs in the kitchen were designed by our colleagues, Martynę Kubal i Marysię Sierpińską?";
			Time.timeScale = 0f;
		} 
	}

	public void OpenFact8(){
		uiFacts.SetActive (!uiFacts.activeSelf);

		if (uiFacts.activeSelf) {
			factsText.text = "How does the PPG Way help us at work? It provides us with a set of shared values that remain consistent regardless of cultural differences.";
			Time.timeScale = 0f;
		} 
	}
	public void OpenFact9(){
		uiFacts.SetActive (!uiFacts.activeSelf);

		if (uiFacts.activeSelf) {
			factsText.text = "The office building has locker rooms with showers for cyclists. You can get details on how to use them at our reception.";
			Time.timeScale = 0f;
		} 
	}

	public void OpenFact10(){
		uiFacts.SetActive (!uiFacts.activeSelf);

		if (uiFacts.activeSelf) {
			factsText.text = "You can bring your dog to the office. During your dog's stay, you are fully responsible for them, and in case of any unexpected issues, you can take refuge in the Myspace room.";
			Time.timeScale = 0f;
		} 
	}

	public void CloseFacts(){
		uiFacts.SetActive (!uiFacts.activeSelf);
		if (!uiFacts.activeSelf) {
			Time.timeScale = 1f;
		} 
	}
}