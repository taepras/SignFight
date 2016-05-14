using UnityEngine;
using System.Collections;

public class ShopButtonController : ButtonController {

	protected override void OnClick () {
		print ("BUTTON CLICKED!");
		Application.LoadLevel ("Shop");
	}
}
