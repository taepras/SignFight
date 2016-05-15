using UnityEngine;
using System.Collections;

public class RoomSkinController : MonoBehaviour {

	public static RoomSkinController instance;

	public Material[] materials;

	private Renderer[] renderers;

	// Use this for initialization
	void Start () {
		instance = this;

		GameStatus.Load ();

		renderers = GetComponentsInChildren<Renderer> ();
		SetMaterial (GameStatus.instance.currentSkinMaterial);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMaterial (Material m) {
		renderers = GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in renderers) {
			r.material = m;
		}
	}
}
