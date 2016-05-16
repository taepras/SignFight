using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeAttackDisplayController : MonoBehaviour {

	public Color colorFull = new Color(0f, 1f, 0f, 0.5f);
	public Color colorEmpty = new Color(1f, 0f, 0f, 0.5f);

	private Image fillImage;
	private float timePerLetter;
	private float startingTime;
	private Slider timeSlider;

	// Use this for initialization
	void Start () {
		fillImage = GetComponentInChildren<Image> ();
		timeSlider = GetComponent<Slider> ();
	}

	// Update is called once per frame
	void Update () {
		SetTimeUI ();
	}
		
	private void SetTimeUI () {
		float timeLeft = TimeAttackGameManager.instance.GetTimeRemaining () / TimeAttackGameManager.instance.GetTimeLimit ();
		timeSlider.value = timeLeft;
		fillImage.color = Color.Lerp (colorEmpty, colorFull, timeLeft);
	}
}
