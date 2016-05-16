using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DangerBlinker : MonoBehaviour {

	private Image image;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		float time = TimeAttackGameManager.instance.GetTimeRemaining ();

		print (time);

		float freq = 0f;
		if (time <= 10f) {
			freq = 10f;
		} else if (time <= 20f) {
			freq = 3f;
		}

		if (freq > 0 && time > 0) {
			float op = Mathf.Sin (Time.time * freq) / 2f + 0.5f;
			image.color = new Color (1f, 1f, 1f, op);
		} else {
			image.color = new Color (1f, 1f, 1f, 0f);
		}
	}
}
