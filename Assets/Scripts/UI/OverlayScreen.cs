using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OverlayScreen : MonoBehaviour {

	public static OverlayScreen instance;

	// Use this for initialization
	void Start () {
		if (instance != null)
			Destroy (gameObject);
		else
			instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//deals with overlay screen

	public void ShowOverlayScreenWithText(string s){
		gameObject.SetActive (true);
		Text t = GetComponentInChildren<Text>();
		t.text = s;
	}

	public void ShowOverlayScreenWithText(string s, float time){
		ShowOverlayScreenWithText (s);
		StartCoroutine (WaitHideOverlayScreen (time));
	}

	public void HideOverlayScreen () {
		gameObject.SetActive (false);
	}

	public void ShowOverlayScreen () {
		gameObject.SetActive (true);
	}

	public void ShowOverlayScreen (float time) {
		gameObject.SetActive (true);
		StartCoroutine (WaitHideOverlayScreen (time));
	}

	IEnumerator WaitHideOverlayScreen (float time) {
		yield return new WaitForSeconds (time);
		HideOverlayScreen ();
	}

	public void ShowEndGameOverlayScreen(){
		string s = "GAME OVER\n" +
		           "\n" +
		           "Score: " + ArcadeGameManager.instance.GetScore () + "\n" +
		           (ArcadeGameManager.instance.GetScore () >= GameStatus.instance.arcadeHighScore ? "HIGH SCORE!\n" : "") +
		           "\n" +
		           "Letters Cleared: " + ArcadeGameManager.instance.GetLettersCleared () + (ArcadeGameManager.instance.GetLettersCleared () >= GameStatus.instance.arcadeHighLettersCleared ? " NEW HIGH" : "") + "\n" +
		           "Words Cleared: " + ArcadeGameManager.instance.GetWordsCleared () + "\n" +
		           "Enemies Killed: " + ArcadeGameManager.instance.GetEnemiesKilled () + (ArcadeGameManager.instance.GetEnemiesKilled () >= GameStatus.instance.arcadeHighEnemiesKilled ? " NEW HIGH" : "") + "\n" +
		           "Longest Combo: " + ArcadeGameManager.instance.GetMaxCombo () + (ArcadeGameManager.instance.GetMaxCombo () >= GameStatus.instance.arcadeHighCombo ? " NEW HIGH" : "") + "\n" +
		           "\n" +
		           "Money Gained: +" + ArcadeGameManager.instance.GetScore () + " = " + GameStatus.instance.money;
		ShowOverlayScreenWithText (s);
	}

	public void ShowTimeAttackEndGameOverlayScreen(){
		string s = "TIME'S UP.\n" +
		           "\n" +
		           "Score: " + TimeAttackGameManager.instance.GetScore () + "\n" +
		           (TimeAttackGameManager.instance.GetScore () >= GameStatus.instance.timeAttackHighScore ? "HIGH SCORE!\n" : "") +
		           "\n" +
		           "Letters Cleared: " + TimeAttackGameManager.instance.GetLettersCleared () + (TimeAttackGameManager.instance.GetLettersCleared () >= GameStatus.instance.timeAttackHighLettersCleared ? " NEW HIGH" : "") + "\n" +
		           "Words Cleared: " + TimeAttackGameManager.instance.GetWordsCleared ();
		ShowOverlayScreenWithText (s);
	}

	public static OverlayScreen GetInstance(){
		if (instance == null)
			instance = new OverlayScreen ();
		return instance;
	}
}
