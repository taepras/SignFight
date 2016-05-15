using UnityEngine;
using System.Collections;

public class PracticeButtonController : ButtonController {

	protected override void OnClick () {
		print ("BUTTON CLICKED!");
		Application.LoadLevel ("Temp/PracticeGame");
	}
}
