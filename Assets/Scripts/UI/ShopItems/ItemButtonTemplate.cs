using UnityEngine;
using System.Collections;

public class ItemButtonTemplate : ButtonController {
	public int itemIndex;
	public Material itemMaterial;
	//public int price;

		
	protected override void OnClick () {
		//ItemController.selectItem (itemMaterial, itemIndex);
		print("call method");
		FindObjectOfType<ItemController>().selectItem(itemMaterial, itemIndex);
		//gameObject.GetComponent<ItemController>().selectItem(itemMaterial, itemIndex);

	}

}
