using UnityEngine;
using System.Collections;

public class ItemController : ButtonController {
	public Transform []itemTrans;
	public Texture onSelectItemImg;

	private Material materialSelected;
	//private Transform []itemTrans;
	private int itemNumber = 1;

	// Use this for initialization
	void Start () {
		/*int numberOfItem = 6;

		items = new GameObject[numberOfItem];
		itemTrans = new Transform[numberOfItem];

		for(int i = 0; i < numberOfItem; i++){
			//itemTrans[i] = items[i].GetComponent<Transform> ();
		}*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect(itemTrans[itemNumber-1].position.x-15,-itemTrans[itemNumber-1].position.y+ Screen.height-15,50,50), onSelectItemImg, ScaleMode.ScaleToFit);
		//GUI.DrawTexture (new Rect(200,200,50,50), onSelectItemImg, ScaleMode.ScaleToFit);

	}

	public void materialSelect(Material m){
		materialSelected = m;
	}

	public void numberSelect(int n){
		itemNumber = n;
	}

	protected override void OnClick(){
		//save material to GameStatus
		Application.LoadLevel ("Menu");
	}
}
