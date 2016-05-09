using UnityEngine;
using System.Collections;

public class QuitButtonController : ButtonController {

	protected override void OnClick () {
		Application.Quit ();
	}
}
