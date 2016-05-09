using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LetterController : MonoBehaviour {

	public int activeFontSize = 300;
	public int inactiveFontSize = 100;
	public int activeScale = 15;
	public int inactiveScale = 6;
	public float moveSpeed = 1000f;
	public float letterSpace = 140f;

	private RectTransform rt;
	private Vector2 centerPosition;
	private Sprite handImage;
	private SpriteRenderer spriteRend = null;
	//private Text displayText = null;

	private char letter = 'A';
	private int position = 0;

	// Use this for initialization
	void Start () {
		//displayText = GetComponent<Text> ();
		rt = GetComponent<RectTransform> ();
		rt.anchoredPosition = new Vector2 (0f, 0f);
		centerPosition = new Vector2 (transform.position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector2.MoveTowards (
			transform.position, 
			new Vector2 (letterSpace * position + centerPosition.x, centerPosition.y), 
			moveSpeed * Time.deltaTime
		);
	}

	public void SetLetter (char letter) {
		this.letter = letter;
		// TODO is this too hack??
		/*if (displayText == null) {
			Start ();
		}
		displayText.text = letter.ToString ();*/
	}

	public void SetPosition (int position) {
		// 0  = in focus
		// <0 = passed
		// >0 = coming next
		this.position = position;
		setFontSize ();
	}

	public void DecreasePosition () {
		position--;
		setFontSize ();
	}

	void setFontSize () {
		// TODO set opacity
		if (position != 0) {
			transform.localScale = new Vector3 (inactiveScale, inactiveScale, 1f);
			GetComponent<Image> ().color = new Color (1f, 1f, 1f, 0.6f);
		} else {
			transform.localScale = new Vector3 (activeScale, activeScale, 1f);
			GetComponent<Image> ().color = new Color (1f, 1f, 1f, 1f);
		}
	}

	public void SetSprite (Sprite img) {
		GetComponent<Image> ().sprite = img;
		handImage = img;
		spriteRend = GetComponent<SpriteRenderer> ();
	}
}
