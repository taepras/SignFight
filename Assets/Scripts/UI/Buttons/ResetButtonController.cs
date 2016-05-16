using UnityEngine;
using System.Collections;

public class ResetButtonController : ButtonController {

	protected override void OnClick () {
		print (Application.loadedLevelName);
		Application.LoadLevel (Application.loadedLevelName);
	}
}
