using UnityEngine;
using System.Collections;

public class GameItemController : MonoBehaviour {

	public float lifeTime = 5f;
	public AudioSource collectAudio;

	private float startTime;
	private bool interactable = true;

	// Use this for initialization
	void Start () {
		collectAudio = GetComponent<AudioSource> ();
		startTime = Time.time;
		OnStart ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime >= lifeTime) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (interactable) {
			collectAudio.Play ();
			OnItemCollected ();
			interactable = false;
			try{
				gameObject.GetComponent<CanvasRenderer> ().SetAlpha (0f);
			} catch (System.Exception e){}
			Destroy (gameObject);
		}
	}

	protected virtual void OnItemCollected () {
		print ("Item Collected");
	}

	protected virtual void OnStart () {
		print ("Item Created");
	}
}
