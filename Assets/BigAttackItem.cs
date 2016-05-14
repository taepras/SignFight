using UnityEngine;
using System.Collections;

public class BigAttackItem : ItemController {
	public float HPUpAmount = 40f;

	void Start(){
		InitPosition ();
	}

	public void InitPosition () {
		gameObject.transform.SetParent(FindObjectOfType<Canvas> ().GetComponent<Transform> ());
		float x = Screen.width / 2 - 70f;
		float y = -Screen.height / 2 + 150f;
		RectTransform rt = GetComponent<RectTransform> ();
		rt.anchoredPosition = new Vector2 (0f, 0f);
		rt.localPosition = new Vector3 (x, y, -3);
	}

	protected override void OnItemCollected () {
		// TODO change big attack prefab
		ArcadeGameManager.instance.player.Fire(100);
	}
}
