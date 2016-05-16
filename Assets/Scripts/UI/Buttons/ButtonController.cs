using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour {

	public float timeToClick = 1f;

	private Slider clickSlider;

	private float timeStartClick = 0;

	private Button button = null;
	private AudioSource selectAudio;
	private bool clicked = false;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button> ();
		selectAudio = GetComponent<AudioSource> ();
		clickSlider = transform.FindChild ("ClickSlider").GetComponent<Slider> ();
		clickSlider.value = 0;
		timeStartClick = Time.time;
		button.onClick.AddListener (ClickAction);
		AfterStart ();
	}

	protected virtual void AfterStart () {
	}

	void OnTriggerEnter2D (Collider2D other) {
		CursorController cursor = other.GetComponent<CursorController> ();
		if (cursor != null && cursor.IsActive ()) {
			GetComponent<Image> ().color = new Color (0.4f, 0.4f, 0.4f, 1f);
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		CursorController cursor = other.GetComponent<CursorController> ();
		if (cursor != null && cursor.IsGrabbing ()) {
			clickSlider.value = (Time.time - timeStartClick) / timeToClick;
			if (Time.time - timeStartClick >= timeToClick && !clicked) {
				//timeStartClick = Time.time;
				clicked = true;
				clickSlider.value = 1f;
				ClickAction ();
			}
		} else {
			timeStartClick = Time.time;
			clickSlider.value = 0;
		}
	}

	IEnumerator ExecuteClick () {
		yield return new WaitForSeconds(selectAudio.clip.length);
		OnClick ();
	}

	void OnTriggerExit2D (Collider2D other){
		CursorController cursor = other.GetComponent<CursorController> ();
		if (cursor != null) {
			GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
			timeStartClick = Time.time;
			clickSlider.value = 0;
			clicked = false;
		}
	}

	protected void ClickAction () {
		selectAudio.Play ();
		StartCoroutine (ExecuteClick());
	}

	virtual protected void OnClick () {
		print ("leap button triggered");
	}
}
