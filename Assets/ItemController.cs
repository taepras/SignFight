using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {

	public float lifeTime = 5f;

	private float startTime;

	// Use this for initialization
	void Start () {
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
		OnItemCollected ();
		Destroy (gameObject);
	}

	protected virtual void OnItemCollected () {
		print ("Item Collected");
	}

	protected virtual void OnStart () {
		print ("Item Created");
	}
}
