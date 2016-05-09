using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class HandLetter {
	
	[SerializeField]
	public string letter;
	public float f0x, f0y, f0z;
	public float f1x, f1y, f1z;
	public float f2x, f2y, f2z;
	public float f3x, f3y, f3z;
	public float f4x, f4y, f4z;
	public Vector3[] fingerPositions;
	public float[] errorMultipliers;
	public float acceptableError;

	public static HandLetter CreateHandLetterFromJson(string json){
		HandLetter hl = JsonUtility.FromJson<HandLetter> (json);
		hl.BuildHandLetter ();
		return hl;
	}

	public HandLetter () {
		BuildHandLetter ();
	}

	public void BuildHandLetter () {
		fingerPositions = new Vector3[5];
		fingerPositions[0] = new Vector3(f0x, f0y, f0z);
		fingerPositions[1] = new Vector3(f1x, f1y, f1z);
		fingerPositions[2] = new Vector3(f2x, f2y, f2z);
		fingerPositions[3] = new Vector3(f3x, f3y, f3z);
		fingerPositions[4] = new Vector3(f4x, f4y, f4z);

		//letter = letterString [0];
	}
}
