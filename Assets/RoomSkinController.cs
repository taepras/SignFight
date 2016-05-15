using UnityEngine;
using System.Collections;

public class RoomSkinController : MonoBehaviour {

	public static RoomSkinController instance;

	public Material[] materials;

	private int currentMatrtialIndex;
	private Renderer[] renderers;

	// Use this for initialization
	void Start () {
		instance = this;

		GameStatus.Load ();
		currentMatrtialIndex = GameStatus.instance.currentSkinIndex;

		renderers = GetComponentsInChildren<Renderer> ();
		foreach (Renderer r in renderers) {
			r.material = materials [currentMatrtialIndex];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
