using UnityEngine;
using System.Collections;

public class HPUpItem : GameItemController {

	public float HPUpAmount = 40f;

	protected override void OnStart () {
		InitPosition ();
	}

	public void InitPosition () {
		gameObject.transform.SetParent(FindObjectOfType<Canvas> ().GetComponent<Transform> ());
		float x = Random.Range (-Screen.width / 2 + 50f, Screen.width / 2 - 50f);
		float y = Random.Range (-Screen.height / 2 + 50f, Screen.height / 2 - 50f);
		RectTransform rt = GetComponent<RectTransform> ();
		rt.anchoredPosition = new Vector2 (0f, 0f);
		rt.localPosition = new Vector3 (x, y, -3);
	}

	protected override void OnItemCollected () {
		ArcadeGameManager.instance.player.IncreaseHealth (HPUpAmount);
		print ("COLLECTED");
	}
}
