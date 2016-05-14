using UnityEngine;
using System.Collections;
using Leap;

public class CursorController : MonoBehaviour {

	public float handHorizontalOffset = 0.15f;
	public float cursorSensitivity = 3000f;
	public float grabThreshold = 0.8f;

	LeapProvider provider;

	private bool handFound = false;
	private bool grabbing = false;
	private bool leftHandOnly = false;
	private RectTransform cursor;


	// Use this for initialization
	void Start () {
		provider = FindObjectOfType<LeapProvider> () as LeapProvider;
		cursor = GetComponent<RectTransform> ();
		cursor.anchoredPosition = new Vector2 (0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		handFound = false;

		Frame frame = provider.CurrentFrame;
		if (frame.Hands.Count > 0) {
			Hand hand = frame.Hands [0];
			handFound = true;
			Vector3 palmPosition = hand.PalmPosition.ToUnity () - provider.transform.position;
			float x = (Quaternion.Inverse(provider.transform.rotation) * palmPosition).x + handHorizontalOffset * (hand.IsLeft ? 1 : -1);
			float y = (Quaternion.Inverse (provider.transform.rotation) * palmPosition).z;
			float z = -3;
			cursor.position = new Vector3 (x * cursorSensitivity, y * cursorSensitivity, z);
			cursor.gameObject.SetActive (true);
			if (hand.GrabStrength > grabThreshold) {
				grabbing = true;
				cursor.gameObject.GetComponent<UnityEngine.UI.Image> ().color = new Color (0f, 1f, 1f, 0.9f);
			} else {
				grabbing = false;
				cursor.gameObject.GetComponent<UnityEngine.UI.Image> ().color = new Color (1f, 1f, 1f, 0.9f);
			}
		}

		foreach (Hand hand in frame.Hands) {
			
		}

		if(!handFound)
			cursor.gameObject.GetComponent<UnityEngine.UI.Image> ().color = new Color (1f, 1f, 1f, 0f);
	}

	public bool IsGrabbing() {
		return grabbing;
	}

	public void ForceLeftHandOnly (bool setting) {
		leftHandOnly = setting;
	}
}
