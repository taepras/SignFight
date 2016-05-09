using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeController : MonoBehaviour {

	public Image fillImage;
	public Color colorFull = Color.green;
	public Color colorEmpty = Color.red;

	private float timePerLetter;
	private float startingTime;
	private Slider timeSlider;

	// Use this for initialization
	void Start () {
		timeSlider = GetComponent<Slider> ();
		startingTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		SetTimeUI ();
	}

	public void ResetTimer () {
		startingTime = Time.time;
	}

	public bool IsTimeUp () {
		return Time.time - startingTime > timePerLetter;
	}

	private void SetTimeUI () {
		float timeLeft = timePerLetter - (Time.time - startingTime);
		timeSlider.value = timeLeft * 100 / timePerLetter;

		fillImage.color = Color.Lerp (colorEmpty, colorFull, timeLeft / timePerLetter);
	}

	public void Show () {
		if (!IsActive ()) {
			ResetTimer ();
		}
		gameObject.SetActive (true);
	}

	public void Hide () {
		gameObject.SetActive (false);
	}

	public bool IsActive () {
		return gameObject.activeSelf;
	}

	public void SetTimePerLetter (float time) {
		this.timePerLetter = time;
	}
}
