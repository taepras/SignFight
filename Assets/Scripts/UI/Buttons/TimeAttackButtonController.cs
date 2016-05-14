using UnityEngine;
using System.Collections;

public class TimeAttackButtonController : ButtonController {

	protected override void OnClick () {
		print ("BUTTON CLICKED!");
		Application.LoadLevel ("TimeAttackGame");
	}
}
