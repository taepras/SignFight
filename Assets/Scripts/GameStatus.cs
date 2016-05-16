using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using UnityEngine;

[Serializable]
public class GameStatus {
	public int arcadeHighScore;
	public int arcadeHighCombo;
	public int arcadeHighLettersCleared;
	public int arcadeHighEnemiesKilled;

	public int timeAttackHighScore;
	public int timeAttackHighLettersCleared;

	public int money;
	public int currentSkinIndex;
	public Material currentSkinMaterial;
	public bool[] unlockedSkin;

	public static GameStatus instance;

	private GameStatus () {}

	public static void Save () {
		string json = JsonUtility.ToJson (instance);
		File.WriteAllText ("save.json", json);
	}

	public static void Load(){
		// TODO remove this test
		try {
			if (instance == null) {
				string json = File.ReadAllText ("save.json");
				instance = JsonUtility.FromJson<GameStatus> (json);
			}
		} catch(System.Exception e){
			GameStatus.Create ();
			GameStatus.Save ();
		}
	}

	public static void Create(){
		instance = new GameStatus ();
		instance.unlockedSkin = new bool[9];
		instance.unlockedSkin [0] = true;
		instance.currentSkinIndex = 0;

		instance.money = 5000; //use in testing
	}
}
