using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WordController : MonoBehaviour {

	public GameObject charDisplayPrefab;
	public Sprite[] handImage;
	public GestureController gestureController;

	private List<LetterController> displayCharacters = new List<LetterController> ();
	private List<GameObject> displayCharactersGO = new List<GameObject> ();

	private List<string> words;
	private int currentWordIndex = 0;
	private int currentCharIndex = 0;
	public int wordsToBeScored = 0;
	private bool keyHit = false;

	// Use this for initialization
	void Start () {
		LoadWords ();
		currentWordIndex = Random.Range (0, words.Count);
		GetNewWord ();
		wordsToBeScored = 0;
	}
	
	// Update is called once per frame
	void Update () {
		keyHit = gestureController.IsPassed (GetCurrentCharacter ());
		if (Config.DEBUG) {
			keyHit = keyHit || Input.GetKeyDown (GetCurrentKeyCode ());
		}
		if (keyHit) {
			gestureController.ResetCorrectTime ();
		}
		//keyHit = gestureController.IsCorrect (GetCurrentCharacter ());
	}

	private char getRandomChar(){
		string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		char c = st[Random.Range(0, st.Length)];
		return c;
	}

	private KeyCode GetCurrentKeyCode() {
		return (KeyCode) System.Enum.Parse(typeof(KeyCode), GetCurrentCharacter().ToString());
	}

	public bool IsKeyHit(){
		return keyHit;
	}

	public bool CheckAndGetNextChar (bool correct) {
		SetThisCharCorrect (correct);
		return NextChar ();
	}

	public void SetThisCharCorrect (bool correct) {
		int k = -1;
		for (int i = 0; i < displayCharacters.Count; i++) {
			if (displayCharacters [i].GetPosition () == 0) {
				k = i;
				break;
			}
		}
		displayCharacters [k].SetCorrect (correct);
	}
		
	public bool NextChar () {
		currentCharIndex++;
		if (currentCharIndex >= GetCurrentWord ().Length) {
			currentCharIndex = 0;
			GetNewWord ();
			return false;
		} else {
			foreach (LetterController dc in displayCharacters) {
				dc.DecreasePosition ();
			}
			return true;
		}
	}

	public char GetCurrentCharacter() {
		return words [currentWordIndex] [currentCharIndex];
	}

	public string GetCurrentWord() {
		return words [currentWordIndex];
	}

	public void GetNewWord () {
		// destroy old words
		wordsToBeScored++;
		foreach (GameObject g in displayCharactersGO) {
			Destroy (g);
		}

		displayCharactersGO.Clear ();
		displayCharacters.Clear();

		currentWordIndex = Random.Range (0, words.Count);
		currentCharIndex = 0;

		for (int i = 0; i < words [currentWordIndex].Length; i++) {
			GameObject go = Instantiate (charDisplayPrefab) as GameObject;
			LetterController lc = go.GetComponent<LetterController> ();
			go.transform.SetParent(this.gameObject.transform);
			// TODO is there a BETTER way?
			int index = words [currentWordIndex] [i] - 'A';
			lc.SetSprite (handImage[index]);
			lc.SetLetter (words [currentWordIndex] [i]);
			lc.SetPosition (i);
			displayCharactersGO.Add(go);
			displayCharacters.Add(lc);
		}
	}

	public bool IsWordFinished () {
		// TODO check conditions...
		int tmp = wordsToBeScored;
		wordsToBeScored = 0;
		return tmp > 0;
	}

	private void LoadWords () {
		var textFile = Resources.Load ("words", typeof(TextAsset)) as TextAsset;
		var textArray = textFile.text.Split ('\n');
		print ("Word list loaded.");
		words = new List<string> (textArray);
		for(int i = 0; i < words.Count; i++){
			words[i] = words[i].Trim ();
		}
	}

	public void Show () {
		gameObject.SetActive (true);
	}

	public void Hide () {
		gameObject.SetActive (false);
		keyHit = false;
	}
}
