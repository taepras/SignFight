using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;
using UnityEngine;

[Serializable]
public class GameStatus {
	public int highScore;
	public int highCombo;
	public int highLettersCleared;
	public int highEnemiesKilled;

	public int money;
	public int currentSkinIndex = 0;
	public Material currentSkinMaterial;
	public bool[] unlockedSkin;

	public static GameStatus instance;

	public static void Save () {
		string json = JsonUtility.ToJson (instance);
		File.WriteAllText ("save.json", json);
	}

	public static void Load(){
		if (instance == null) {
			string json = File.ReadAllText ("save.json");
			instance = JsonUtility.FromJson<GameStatus> (json);
		}
	}

	public static void Create(){
		instance = new GameStatus ();
	}
}
