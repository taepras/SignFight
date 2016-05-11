using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ArcadeGameManager : MonoBehaviour {

	public static bool DEBUG = true;

	public float startDelay = 3f;
	public float timePerLetter = 2f;
	public Text scoreText;
	public WordController wordController;
	public TimeController timeController;
	public HealthController enemyHealthController;
	public Vector3 enemySpawnPoint = new Vector3(0f, 0f, 10f);
	public Text uiText;

	public float wordPauseTime = 1f;

	public GameObject[] enemyPrefabs;
	public EnemyController enemy;
	public PlayerController player;

	// TODO move to, say, player controller?
	public Rigidbody fireballPrefab;

	public int scorePerLetter = 1;
	public int scorePerWord = 5;

	// scoring
	private int lettersCleared = 0;
	private int lettersMissed = 0;
	private int wordsCleared = 0;
	private int wordsMissed = 0;

	private bool hasMissedThisWord = false;
	private int lettersClearedThisWord = 0;

	private float startTime = 0;
	private float uiStartHideTime = 0;
	private float uiHideTime = 0;

	// Use this for initialization
	void Start () {
		// TODO count down before start
		uiText.text = "";
		CreateNewEnemy ();
		HideUI (startDelay);
		timeController.SetTimePerLetter (timePerLetter);
	}
	
	// Update is called once per frame
	void Update () {
		if (wordController.IsKeyHit () || (timeController.IsActive() && timeController.IsTimeUp())) {
			if (timeController.IsTimeUp ()) {
				lettersMissed++;
				hasMissedThisWord = true;
				enemy.Fire ();
			} else {
				lettersCleared++;
				lettersClearedThisWord++;
				//player.Fire ();
			}

			timeController.ResetTimer ();

			bool isWordFinished = !wordController.NextChar ();
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
			HideUI (startDelay);
			OnEnemyDead ();
		}

		if (player.IsDead ()) {
			HideUI (1000f);
			uiText.text = "GAME OVER";
		}

		scoreText.text = CalculateScore ().ToString ();
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

	private void OnEnemyDead () {
		// TODO DEALS WITH DA REBIRTH
		//enemy.OnDeath ();
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
}
