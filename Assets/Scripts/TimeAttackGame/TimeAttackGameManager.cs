using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeAttackGameManager : MonoBehaviour {

	public static TimeAttackGameManager instance = null;

	public float startDelay = 4f;
	public float maxTimePerLetter = 3f;
	public float minTimePerLetter = 1.5f;
	public Text timeText;
	public WordController wordController;
	public TimeController timeController;

	public Text uiText;
	public OverlayScreen overlayScreen;

	public float wordPauseTime = 0f;


	public GameObject HPUpItemPrefab;
	public GameObject BigAttackItemPrefab;

	// TODO move to, say, player controller?
	public Rigidbody fireballPrefab;

	public int scorePerLetter = 1;
	public int scorePerWord = 5;
	public int comboToActivateBigAttack = 20;

	// scoring
	private int lettersCleared = 0;
	private int lettersMissed = 0;
	private int wordsCleared = 0;
	private int wordsMissed = 0;
	private int enemiesKilled = 0;
	private int combo;
	private int maxCombo = 0;

	private bool hasMissedThisWord = false;
	private int lettersClearedThisWord = 0;
	private bool bigAttackShown = false;

	private float startTime = 0;
	private float uiStartHideTime = 0;
	private float uiHideTime = 0;

	private bool moneyAdded = false;

	private float stTime;

	private int timeNow;

	// Use this for initialization
	void Start () {

		overlayScreen.ShowOverlayScreenWithText ("Get as many words as possible\nIN 60 SECONDS!", startDelay - 1f);

		// TODO remove this test
		GameStatus.Load ();

		// TODO count down before start
		uiText.text = "";
		HideUI (startDelay);
		timeController.SetTimePerLetter (maxTimePerLetter);
		if (instance == null)
			instance = this;
		else
			Destroy (this);
		timeText.text = "60";
		stTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (wordController.IsKeyHit () || (timeController.IsActive() && timeController.IsTimeUp())) {
			
			bool isCorrectChar = false;

			if (timeController.IsTimeUp ()) {
				lettersMissed++;
				hasMissedThisWord = true;
				combo = 0;
				bigAttackShown = false;
			} else {
				lettersCleared++;
				lettersClearedThisWord++;
				combo++;
				maxCombo = Mathf.Max (combo, maxCombo);
				if (combo > 0 && combo % comboToActivateBigAttack == 0 && FindObjectOfType<BigAttackItem>() == null) {
					Instantiate (BigAttackItemPrefab);
					bigAttackShown = true;
				}
				isCorrectChar = true;
			}

			timeController.ResetTimer ();
			timeController.SetTimePerLetter (GetTimePerLetterFromCombo(combo));

			bool isWordFinished = !wordController.CheckAndGetNextChar (isCorrectChar);
			if (isWordFinished) {
				if(lettersClearedThisWord > 0)


				lettersClearedThisWord = 0;
				if (hasMissedThisWord)
					wordsMissed++;
				else
					wordsCleared++;
				hasMissedThisWord = false;
				HideUI (wordPauseTime);
			}
		}


		timeNow = (int) (61f - (Time.time - stTime));

		if (timeNow < 0) {
			HideUI (100000f);
			overlayScreen.ShowTimeAttackEndGameOverlayScreen();

			// save player high stats
			GameStatus.instance.timeAttackHighScore = Mathf.Max (GameStatus.instance.timeAttackHighScore, CalculateScore());
			GameStatus.instance.timeAttackHighLettersCleared = Mathf.Max (GameStatus.instance.timeAttackHighLettersCleared, GetLettersCleared());
			GameStatus.instance.timeAttackHighWordCleared = Mathf.Max (GameStatus.instance.timeAttackHighWordCleared, GetWordsCleared());

			GameStatus.Save ();
		} else {
			timeText.text = timeNow.ToString();
		}
	





	

		DisplayUI ();
	}

	private int CalculateScore () {
		return lettersCleared * scorePerLetter + wordsCleared * scorePerWord;
	}

	private void DisplayUI () {
		if (Time.time - uiStartHideTime > uiHideTime) {
			wordController.Show ();
			timeController.Show ();
		} else {
			wordController.Hide ();
			timeController.Hide ();
		}
	}


	// TODO change this method to "DisableGame()"
	private void HideUI (float time = 0f) {
		uiHideTime = time;
		uiStartHideTime = Time.time;
	}





	public int GetCombo () {
		return combo;
	}

	public int GetTimeRemaining () {
		return timeNow;
	}

	public int GetMaxCombo () {
		return maxCombo;
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

	public int GetEnemiesKilled(){
		return enemiesKilled;
	}

	private float GetTimePerLetterFromCombo (int combo) {
		float shift = 4f;
		float rate = 1f;
		float diff = 1 / (1 + Mathf.Exp (shift - combo / rate));
		float t = maxTimePerLetter - diff * (maxTimePerLetter - minTimePerLetter);
		return t;
	}
}
