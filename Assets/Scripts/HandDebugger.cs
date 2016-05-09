using UnityEngine;
using System.Collections;

public class HandDebugger : MonoBehaviour {

	public GestureController gc;
	public Transform[] fingerTips;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 5; i++) {
			fingerTips [i].localPosition = gc.normalizedFingerPositions [i] * 20;
		}
	}
}
