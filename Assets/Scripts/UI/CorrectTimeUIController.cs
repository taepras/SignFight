using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CorrectTimeUIController : MonoBehaviour {

	private Slider correctTimeSlider;

	// Use this for initialization
	void Start () {
		correctTimeSlider = GetComponent<Slider> ();
	}

	// Update is called once per frame
	void Update () {
		//SetAccuracyUI ();
	}

	public void SetCorrectTimePercentage (float percentage) {
		correctTimeSlider.value = percentage;
	}
}
