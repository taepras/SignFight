using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemController : ButtonController {
	public Transform []itemTrans;
	public Texture onSelectItemImg;
	public Text moneyText;
	public int money;

	private int currentSkinIndex;
	private Material currentSkinMaterial;
	private bool[] unlockedSkin;
	private int[] price = {0, 300, 100, 100, 100, 100}; //still not sure about this

	// Use this for initialization
	protected override void AfterStart () {
		GameStatus.Load ();
		money = GameStatus.instance.money;
		moneyText.text = "Money : " + money;
		currentSkinIndex = 0;//GameStatus.instance.currentSkinIndex;
		//unlockedSkin = GameStatus.instance.unlockedSkin;

		//only for test--------
		unlockedSkin = new bool[6];
		for (int i = 0; i < unlockedSkin.Length; i++)
			unlockedSkin [i] = true;
		//---------------------
	}


	void OnGUI(){
		//draw mask on current skin
		GUI.DrawTexture (
			new Rect (
				itemTrans [currentSkinIndex].position.x - 15,
				-itemTrans [currentSkinIndex].position.y + Screen.height - 15, 50, 50
			), 
			onSelectItemImg, ScaleMode.ScaleToFit
		);

		//draw price on loked skin
		for (int i = 0; i < unlockedSkin.Length; i++) {
			if (!unlockedSkin [i]) {
				GUI.TextArea (new Rect(itemTrans[i].position.x-15,-itemTrans[i].position.y+ Screen.height-15,50,50), price[i] + " Point");
			}
		}

	}

	//player selected item
	public void selectItem(Material m, int index){
		print("method called!");
		if (!unlockedSkin [index]) {
			//buy item & set skin
			if(money < price[index]) return; //not enough money
			money -= price[index];
			currentSkinIndex = index;
			currentSkinMaterial = m;
			unlockedSkin [index] = true;
		
		} else {
			//set skin
			currentSkinIndex = index;
			currentSkinMaterial = m;
		}
	}

	protected override void OnClick(){
		//save material to GameStatus
		GameStatus.instance.currentSkinIndex = currentSkinIndex;
		GameStatus.instance.currentSkinMaterial = currentSkinMaterial;
		GameStatus.instance.money = money;
		GameStatus.instance.unlockedSkin = unlockedSkin;
		GameStatus.Save ();
		Application.LoadLevel ("Menu");
	}
}
