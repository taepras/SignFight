using UnityEngine;
using System.Collections;

public class ChangePageButtonController : ButtonController {

	public string sceneName;

	protected override void OnClick () {
		Application.LoadLevel (sceneName);
	}
}
