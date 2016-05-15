using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollController : MonoBehaviour {
	public GameObject scrollbar;
	public RectTransform panel;
	public float speed = 1;
	public Transform cursor;

	private Scrollbar myscrollbar;

	// Use this for initialization
	void Start () {
		myscrollbar = scrollbar.GetComponent<Scrollbar> ();

	}
	
	// Update is called once per frame
	void Update () {
		//Scroll up/down if input is at top/buttom
		if (myscrollbar.value > 0 && cursor.position.y < panel.position.y - panel.rect.height / 2 && cursor.position.y > 0) {
			myscrollbar.value -= 0.1f * speed;
		} else if (myscrollbar.value < 1 && cursor.position.y > panel.position.y + panel.rect.height / 2 && cursor.position.y < Screen.height) {
			myscrollbar.value += 0.1f * speed;
		}
		
	}
}

