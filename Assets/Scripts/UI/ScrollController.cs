using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollController : MonoBehaviour {
	public GameObject scrollbar;
	public RectTransform panel;
	public float speed = 1;

	private Scrollbar myscrollbar;

	// Use this for initialization
	void Start () {
		myscrollbar = scrollbar.GetComponent<Scrollbar> ();
		//print ("panel x = " + panel.position.x);
		//print ("panel y = " + panel.position.y);
		//print ("panel h = " + panel.rect.height);
		//print ("panel w = " + panel.rect.width);
	}
	
	// Update is called once per frame
	void Update () {
		//print (Input.mousePosition.x + "," + Input.mousePosition.y);
		if (myscrollbar.value > 0 && Input.mousePosition.y < panel.position.y - panel.rect.height / 2) {
			myscrollbar.value -= 0.1f * speed;
		} else if (myscrollbar.value < 1 && Input.mousePosition.y > panel.position.y + panel.rect.height / 2) {
			myscrollbar.value += 0.1f * speed;
		}
		
	}
}

