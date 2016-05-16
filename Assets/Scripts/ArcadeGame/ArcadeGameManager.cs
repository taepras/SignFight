using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArcadeGameManager : MonoBehaviour {

	public static ArcadeGameManager instance = null;

	public float startDelay = 4f;
	public float maxTimePerLetter = 3f;
	public float minTimePerLetter = 1.5f;
	public Text scoreText;
	public WordController wordController;
	public TimeController timeController;
	public HealthController enemyHealthController;
	public Vector3 enemySpawnPoint = new Vector3(0f, 0f, 10f);
	public Text uiText;
	public OverlayScreen overlayScreen;

	public float wordPauseTime = 1f;

	public GameObject[] enemyPrefabs;
	public EnemyController enemy;
	public PlayerController player;
	public GameObject HPUpItemPrefab;
	public GameObject BigAttackItemPrefab;

	// TODO move to, say, player controller?
	public Rigidbody fireballPrefab;

	public int scorePerLetter = 1;
	public int scorePerWord = 5;
	public int scorePerEnemy = 10;
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

	// Use this for initialization
	void Start () {

		overlayScreen.ShowOverlayScreenWithText ("Survive!", startDelay - 1f);

		GameStatus.Load ();

		// TODO count down before start
		uiText.text = "";
		CreateNewEnemy ();
		HideUI (startDelay);
		timeController.SetTimePerLetter (maxTimePerLetter);
		if (instance == null)
			instance = this;
		else
			Destroy (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (wordController.IsKeyHit () || (timeController.IsActive() && timeController.IsTimeUp())) {
			
			bool isCorrectChar = false;

			if (timeController.IsTimeUp ()) {
				lettersMissed++;
				hasMissedThisWord = true;
				enemy.Fire ();
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
					player.Fire (lettersClearedThisWord);
				lettersClearedThisWord = 0;
				if (hasMissedThisWord)
					wordsMissed++;
				else
					wordsCleared++;
				hasMissedThisWord = false;
				HideUI (wordPauseTime);
			}
		}

		if (enemy.IsDead ()) {
			enemiesKilled++;
			HideUI (startDelay);
			OnEnemyDead ();
		}

		if (player.IsDead ()) {
			HideUI (100000f);
			//uiText.text = "GAME OVER";

			overlayScreen.ShowEndGameOverlayScreen ();
			
			// save player high stats
			GameStatus.instance.arcadeHighScore = Mathf.Max (GameStatus.instance.arcadeHighScore, CalculateScore());
			GameStatus.instance.arcadeHighCombo = Mathf.Max (GameStatus.instance.arcadeHighCombo, maxCombo);
			GameStatus.instance.arcadeHighLettersCleared = Mathf.Max (GameStatus.instance.arcadeHighLettersCleared, lettersCleared);
			GameStatus.instance.arcadeHighEnemiesKilled = Mathf.Max (GameStatus.instance.arcadeHighEnemiesKilled, enemiesKilled);

			if (!moneyAdded) {
				GameStatus.instance.arcadeEnemiesKilled += enemiesKilled;
				GameStatus.instance.arcadeLettersCleared += lettersCleared;
				GameStatus.instance.money += CalculateScore ();
				moneyAdded = true;
			}
			GameStatus.Save ();
		} else {
			float p = Random.Range (0f, 4000f) * Mathf.Pow(player.GetHealthPercentage () / 100f, 2);
			if (p < 1f && FindObjectOfType<HPUpItem> () == null) {
				Instantiate (HPUpItemPrefab);
			}
		}

		scoreText.text = CalculateScore ().ToString ();
		DisplayUI ();
	}

	private int CalculateScore () {
		return lettersCleared * scorePerLetter + wordsCleared * scorePerWord + enemiesKilled * scorePerEnemy;
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

	private void OnEnemyDead () {
		CreateNewEnemy ();
	}

	private void CreateNewEnemy () {
		GameObject enemyGO = Instantiate (
			enemyPrefabs [Random.Range (0, enemyPrefabs.Length)], 
			enemySpawnPoint,
			Quaternion.Euler (new Vector3 (0, 180, 0))
		) as GameObject;
		enemy = enemyGO.GetComponent<EnemyController> ();
		enemy.SetPlayer (player);
		enemy.SetHealthDisplay (enemyHealthController);
		player.SetEnemy (enemy);
	}

	public int GetCombo () {
		return combo;
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
