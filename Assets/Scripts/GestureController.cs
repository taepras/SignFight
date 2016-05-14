using UnityEngine;
using System.Collections;
using Leap;

public class GestureController : MonoBehaviour {

	public float gestureSensitivity = 7f;
	public float timeToCorrect = 0.4f;

	public AccuracyUIController accUI;
	public CorrectTimeUIController correctUI;

	private HandLetter[] handLetters;
	public Vector3[] normalizedFingerPositions;
	private float accPercentage = 0f;
	private char refChar = 'A';
	private float timeStartCorrect = 0;
	private bool grabbing = false;

	LeapProvider provider;

	// Use this for initialization
	void Start () {
		provider = FindObjectOfType<LeapProvider> () as LeapProvider;
		LoadHandLetters ();
		normalizedFingerPositions = new Vector3[5];
	}
	
	// Update is called once per frame
	void Update () {
		bool rightHandFound = false;
		bool leftHandFound = false;

		Frame frame = provider.CurrentFrame;
		foreach (Hand hand in frame.Hands)
		{
			if (hand.IsRight)
			{
				rightHandFound = true;
				UpdateNormalizedFingerPositions (hand);
				// TODO fix json file for scale
				for (int i = 0; i < 5; i++) {
					normalizedFingerPositions [i] *= 5;
				}
				accPercentage = GetAccuracyPercentage (refChar);
			}
		}

		if (!rightHandFound) {
			accPercentage = 0;
		}
			
		//Debug.Log("char: " + refChar + ", isCorrect: " + IsCorrect(refChar) + ", timeToPass: " + GetTimeLeftToPass() + ", isPassed: " + IsPassed(refChar));
		accUI.SetAccuracyPercentage (accPercentage);

		if (!IsCorrect (refChar)) {
			ResetCorrectTime ();
			correctUI.SetCorrectTimePercentage (0);
		} else {
			correctUI.SetCorrectTimePercentage (GetTimeHaveCorrected () / timeToCorrect * 100);
		}
	}

	private void LoadHandLetters () {
		handLetters = new HandLetter[26];

		for (int i = 0; i < 26; i++) {
			char currentChar = 'A';
			currentChar += (char) i;
			var jsonFile = Resources.Load ("hands/hand_" + currentChar, typeof(TextAsset)) as TextAsset;
			string json = jsonFile.text.Replace ("\n", "").Replace ("\t", "").Replace (" ", "");
			print (json);
			handLetters [i] = HandLetter.CreateHandLetterFromJson(json);
		}	
	}

	public bool IsCorrect (char refChar) {
		return GetAccuracyPercentage (refChar) > 99f;
	}

	public bool IsPassed (char refChar) {
		this.refChar = refChar;
		return Time.time - timeStartCorrect >= timeToCorrect;
	}

	public float GetTimeLeftToPass () {
		return timeToCorrect - (Time.time - timeStartCorrect);
	}

	public float GetTimeHaveCorrected () {
		return (Time.time - timeStartCorrect);
	}

	public void ResetCorrectTime (){
		timeStartCorrect = Time.time;
	}

	private void UpdateNormalizedFingerPositions(Hand hand){
		Vector3 o = hand.PalmPosition.ToUnity ();
		Vector3 yy = hand.Direction.ToUnity ();
		Vector3 zz = hand.PalmNormal.ToUnity ();
		Vector3 xx = Vector3.Cross (yy, zz);

		//Vector3[] normalizedFingers = new Vector3[5];

		for (int i = 0; i < 5; i++) {
			Vector3 p = hand.Fingers [i].TipPosition.ToUnity ();
			Vector3 pp = p - o;

			float px = Vector3.Dot (pp, xx);
			float py = Vector3.Dot (pp, yy);
			float pz = Vector3.Dot (pp, zz);

			Vector3 pnew = new Vector3 (px, py, pz);

			normalizedFingerPositions[i] = pnew;
		}

		//return normalizedFingers;
	}

	float CalculateError (Vector3[] referencePositions, Vector3[] testPositions, float[] multipliers = null) {
		if (multipliers == null) {
			multipliers = new float[5];
			for (int i = 0; i < 5; i++) {
				multipliers [i] = 1f;
			}
		}

		float sumError = 0;
		for (int i = 0; i < 5; i++) {
			float e = Vector3.Distance (referencePositions [i], testPositions [i]) * multipliers [i];
			sumError += Mathf.Abs(e);
		}
		return sumError;
	}

	public float GetAccuracyPercentage (char refChar) {
		this.refChar = refChar;

		HandLetter h = handLetters [refChar - 'A'];
		float error = CalculateError (h.fingerPositions, normalizedFingerPositions, h.errorMultipliers);

		//TODO fine-tune errors

		float bestFit = h.acceptableError * gestureSensitivity;
		float maxError = h.acceptableError * gestureSensitivity * 5;
		float ratio = (maxError - error) / (maxError - bestFit);
		ratio = Mathf.Clamp (ratio, 0f, 1f);

		//print ("char : " + refChar + ", error: " + error + ", %: " + ratio * 100);

		return ratio * 100;
	}
}
