using UnityEngine;
using System.Collections;

public class ItemButtonTemplate : ButtonController {
	public int itemIndex;
	public Material itemMaterial;
	//public int price;

		
	protected override void OnClick () {
		//ItemController.selectItem (itemMaterial, itemIndex);
		print("call method");
		gameObject.GetComponent<ItemController>().selectItem(itemMaterial, itemIndex);

	}

}
