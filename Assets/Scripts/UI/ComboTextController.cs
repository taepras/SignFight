using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboTextController : MonoBehaviour {

	private Text t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		int combo = TimeAttackGameManager.instance.GetCombo ();
		if (combo <= 0)
			t.text = "";
		else
			t.text = "COMBO x" + combo;
	}
}
