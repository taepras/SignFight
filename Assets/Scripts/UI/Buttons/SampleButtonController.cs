using UnityEngine;
using System.Collections;

public class SampleButtonController : ButtonController {

	protected override void OnClick () {
		print ("BUTTON CLICKED!");
		Application.LoadLevel ("Game");
	}
}
