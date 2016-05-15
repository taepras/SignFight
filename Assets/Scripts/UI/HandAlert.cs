using UnityEngine;
using System.Collections;
using Leap;

public class HandAlert : MonoBehaviour {

	LeapProvider provider;

	private CanvasRenderer cr;
	// Use this for initialization
	void Start () {
		provider = FindObjectOfType<LeapProvider> () as LeapProvider;	
		cr = GetComponent<CanvasRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (provider.CurrentFrame.Hands.Count == 0){
			cr.SetAlpha (1f);
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild (i).GetComponent<CanvasRenderer> ().SetAlpha (1f);
			}
		}else{
			cr.SetAlpha (0f);
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild (i).GetComponent<CanvasRenderer> ().SetAlpha (0f);
			}
		}
	}
}
