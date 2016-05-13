using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

	public float timeToClick = 1f;

	private Slider clickSlider;

	private float timeStartClick = 0;

	[SerializeField] private Button button = null;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		clickSlider = transform.FindChild ("ClickSlider").GetComponent<Slider> ();
		clickSlider.value = 0;
		timeStartClick = Time.time;
		button.onClick.AddListener (OnClick);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		CursorController cursor = other.GetComponent<CursorController> ();
		if (cursor != null) {
			GetComponent<Image> ().color = new Color (0.8f, 0.8f, 0.8f, 1f);
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		CursorController cursor = other.GetComponent<CursorController> ();
		if (cursor != null && cursor.IsGrabbing ()) {
			clickSlider.value = (Time.time - timeStartClick) / timeToClick;
			if (Time.time - timeStartClick >= timeToClick) {
				timeStartClick = Time.time;
				OnClick ();
			}
		} else {
			timeStartClick = Time.time;
			clickSlider.value = 0;
		}
	}

	void OnTriggerExit2D (Collider2D other){
		CursorController cursor = other.GetComponent<CursorController> ();
		if (cursor != null) {
			GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
			timeStartClick = Time.time;
			clickSlider.value = 0;
		}
	}

	virtual protected void OnClick () {
		print ("leap button triggered");
	}
}
