using UnityEngine;
using System.Collections;

public class ArcadeButtonController : ButtonController {

	protected override void OnClick () {
		print ("BUTTON CLICKED!");
		Application.LoadLevel ("ArcadeGame");
	}
}
