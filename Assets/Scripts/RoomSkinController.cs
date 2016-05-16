using UnityEngine;
using System.Collections;

public class RoomSkinController : MonoBehaviour {

	public static RoomSkinController instance;

	public Material[] materials;

	private int CurrentMaterialIndex;
	private Renderer[] renderers;

	// Use this for initialization
	void Start () {
		instance = this;

		GameStatus.Load ();
		CurrentMaterialIndex = GameStatus.instance.currentSkinIndex;

		renderers = GetComponentsInChildren<Renderer> ();
		SetMaterial (CurrentMaterialIndex);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMaterial (int index) {
		renderers = GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in renderers) {
			r.material = materials[index];
		}
	}
}
