using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PracticeGameManager : MonoBehaviour {

	public static PracticeGameManager instance = null;

	public float startDelay = 3f;
	public float timePerLetter = 2f;
	public Text scoreText;
	public WordController wordController;
	public Text uiText;
	public Text detailedScoreText;
	public OverlayScreen overlayScreen;

	public float wordPauseTime = 1f;

	public PlayerController player;

	// TODO move to, say, player controller?

	public int scorePerLetter = 1;
	public int scorePerWord = 5;
	public int scorePerEnemy = 10;

	// scoring
	private int lettersCleared = 0;
	private int wordsCleared = 0;

	private float startTime = 0;
	private float uiStartHideTime = 0;
	private float uiHideTime = 0;

	// Use this for initialization
	void Start () {

		overlayScreen.ShowOverlayScreenWithText ("PRACTICE MODE", startDelay - 1f);

		// TODO remove this test
		try {
			GameStatus.Load ();
			print (JsonUtility.ToJson(GameStatus.instance));
		} catch(System.Exception e){
			GameStatus.Create ();
			GameStatus.Save ();
		}

		// TODO count down before start
		HideUI (startDelay);
		if (instance == null)
			instance = this;
		else
			Destroy (this);
	}

	// Update is called once per frame
	void Update () {
		if (wordController.IsKeyHit ()) {
			lettersCleared++;

			bool isWordFinished = !wordController.NextChar ();
			if (isWordFinished) {
				wordsCleared++;
				player.Fire (0f);
				HideUI (wordPauseTime);
			}
		}

		scoreText.text = CalculateScore ().ToString ();
		detailedScoreText.text = "Letters: " + lettersCleared + "   Words: " + wordsCleared;
		DisplayUI ();
	}

	private int CalculateScore () {
		return lettersCleared * scorePerLetter + wordsCleared * scorePerWord;
	}

	private void DisplayUI () {
		if (Time.time - uiStartHideTime > uiHideTime) {
			wordController.Show ();
		} else {
			wordController.Hide ();
		}
	}


	// TODO change this method to "DisableGame()"
	private void HideUI (float time = 0f) {
		uiHideTime = time;
		uiStartHideTime = Time.time;
	}

	public int GetScore(){
		return CalculateScore ();
	}

	public int GetLettersCleared(){
		return lettersCleared;
	}

	public int GetWordsCleared(){
		return wordsCleared;
	}
}
