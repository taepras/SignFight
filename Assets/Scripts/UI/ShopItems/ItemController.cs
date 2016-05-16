using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemController : ButtonController {
	public GameObject []items;
	//public Texture onSelectItemImg;
	public Text moneyText;
	public int money;

	private int currentSkinIndex;
	private Material currentSkinMaterial;
	private bool[] unlockedSkin;
	private int[] price = {0, 3000, 1000, 1000, 1000, 2000, 2500,4000,3000}; //want to use set pricing in AfterStart but bug

	// Use this for initialization
	protected override void AfterStart () {
		GameStatus.Load ();
		money = GameStatus.instance.money;
		moneyText.text = "Money : " + money;
		currentSkinIndex = GameStatus.instance.currentSkinIndex;
		unlockedSkin = GameStatus.instance.unlockedSkin;

		/* for testing
		unlockedSkin [0] = true;
		for (int i = 1; i < unlockedSkin.Length; i++) {
			unlockedSkin [i] = false;
		}
		*/
			
		//set pricing
		/*
		price [0] = 0;
		for (int i = 1; i < unlockedSkin.Length; i++) {
			Text priceText = items [i].transform.FindChild ("Price").GetComponent<Text>();
			string priceString = priceText.ToString();
			int thisprice = int.Parse (
				priceString.Substring (0, priceString.IndexOf (" "))
			);
			price [i] = thisprice;
		}
		*/

	}

	void Update(){
		moneyText.text = "Money : " + money;
		for (int i = 0; i < unlockedSkin.Length; i++) {
			if (unlockedSkin [i] && i != currentSkinIndex) {
				items [i].transform.FindChild ("tickImage").gameObject.SetActive (false);
				items [i].transform.FindChild ("Price").gameObject.SetActive (false);
			} else if (unlockedSkin [i] && i == currentSkinIndex) {
				items [i].transform.FindChild ("tickImage").gameObject.SetActive (true);
				items [i].transform.FindChild ("Price").gameObject.SetActive (false);
			} else if (!unlockedSkin[i]) {
				items [i].transform.FindChild ("tickImage").gameObject.SetActive (false);
				items [i].transform.FindChild ("Price").gameObject.SetActive (true);
			}
		}
	}


	void OnGUI(){
		//draw mask on current skin
		/*
		GUI.DrawTexture (
			new Rect (
				itemTrans [currentSkinIndex].position.x - 15,
				-itemTrans [currentSkinIndex].position.y + Screen.height - 15, 
				80, 
				80
			), 
			onSelectItemImg, ScaleMode.ScaleToFit
		);

		//draw price on loked skin
		for (int i = 0; i < unlockedSkin.Length; i++) {
			if (!unlockedSkin [i]) {
				GUI.TextArea (new Rect(itemTrans[i].position.x-15,-itemTrans[i].position.y+ Screen.height-15,50,50), price[i] + " Point");
			}
		}
		*/

	}

	//player selected item
	public void selectItem(Material m, int index){
		if (!unlockedSkin [index]) {
			//buy item & set skin
			if(money < price[index]) return; //not enough money
			money -= price[index];
			currentSkinIndex = index;
			currentSkinMaterial = m;
			unlockedSkin [index] = true;
			RoomSkinController.instance.SetMaterial (index);
			GameStatus.instance.currentSkinIndex = currentSkinIndex;
			GameStatus.instance.currentSkinMaterial = currentSkinMaterial;
			GameStatus.instance.money = money;
			GameStatus.instance.unlockedSkin = unlockedSkin;
			GameStatus.Save ();
		
		} else {
			//set skin
			currentSkinIndex = index;
			currentSkinMaterial = m;
			RoomSkinController.instance.SetMaterial (index);
		}
	}

	protected override void OnClick(){
		//save material to GameStatus

		Application.LoadLevel ("Menu");
	}
}
