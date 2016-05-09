using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AccuracyUIController : MonoBehaviour {

	public Image fillImage;
	public Color colorFull = Color.green;
	public Color colorEmpty = Color.red;

	public float maxError = 0.1f;
	private float acceptableError = 0.02f;
	private float currentError = 0.09f;

	private Slider accuracySlider;

	// Use this for initialization
	void Start () {
		accuracySlider = GetComponent<Slider> ();
	}

	// Update is called once per frame
	void Update () {
		//SetAccuracyUI ();
	}

	public void SetAccuracyPercentage (float percentage) {
		accuracySlider.value = percentage;
		fillImage.color = Color.Lerp (colorEmpty, colorFull, percentage / 100);
	}

	public void SetAcceptableError (float error) {
		acceptableError = error;
	}

	public void SetCurrentError (float error) {
		currentError = error;
	}

	public void SetMaxError (float error) {
		maxError = error;
	}

	private void SetAccuracyUI () {
		float accuracyRatio = (maxError - currentError) / (maxError - acceptableError);
		accuracySlider.value = accuracyRatio * 100;

		fillImage.color = Color.Lerp (colorEmpty, colorFull, accuracyRatio);
	}
}
