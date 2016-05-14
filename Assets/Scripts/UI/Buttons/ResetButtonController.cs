using UnityEngine;
using System.Collections;

public class ResetButtonController : ButtonController {

	protected override void OnClick () {
		Application.LoadLevel (Application.loadedLevel);
	}
}
