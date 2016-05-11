using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	public Image fillImage;
	public Color colorFull = new Color(0f, 1f, 0f, 0.5f);
	public Color colorEmpty = new Color(1f, 0f, 0f, 0.5f);

	private Slider healthSlider;

	// Use this for initialization
	void Start () {
		healthSlider = GetComponent<Slider> ();
	}

	// Update is called once per frame
	void Update () {
		fillImage.color = Color.Lerp (colorEmpty, colorFull, healthSlider.value / 100f);
	}
}
