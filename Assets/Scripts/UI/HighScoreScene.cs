using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreScene : MonoBehaviour {
	public Text unlockNumber;
	public Text timeAttackLetterCleard;
	public Text timeAttackHighestScore;
	public Text ArcadeHighestScore;
	public Text ArcadeHighestCombo;
	public Text ArcadeLetterCleared;
	public Text ArcadeEnemiesKilled;

	// Use this for initialization
	void Start () {
		print ("!!!!!");
		GameStatus.Load ();

		bool[] unlockedSkin = GameStatus.instance.unlockedSkin;
		int unlockedSkinCount = 0;
		for (int i = 0; i < unlockedSkin.Length; i++)
			if (unlockedSkin [i])
				unlockedSkinCount++;

		unlockNumber.text = unlockedSkinCount + "/9";

		timeAttackLetterCleard.text = "Letters Cleared: " + GameStatus.instance.timeAttackHighLettersCleared;
		timeAttackHighestScore.text = "Highest Score: " + GameStatus.instance.timeAttackHighScore;
		ArcadeHighestScore.text = "Highest Score: " + GameStatus.instance.arcadeHighScore;
		ArcadeHighestCombo.text = "Highest Combo: " + GameStatus.instance.arcadeHighCombo;
		ArcadeLetterCleared.text = "Letters Cleared: " + GameStatus.instance.arcadeLettersCleared;
		ArcadeEnemiesKilled.text = "Enemies Killed: " + GameStatus.instance.arcadeEnemiesKilled;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
