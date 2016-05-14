using UnityEngine;
using System.Collections;

public class ExitButtonController : ButtonController {

	protected override void OnClick () {
		print ("BUTTON CLICKED!");
		Application.Quit();
	}
}
