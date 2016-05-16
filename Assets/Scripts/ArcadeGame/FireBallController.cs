using UnityEngine;
using System.Collections;

public class FireBallController : MonoBehaviour {

	public GameObject explosionPrefab;
	public float speed = 10f;
	public float damage = 10f;

	private Rigidbody rb;
	private Renderer rd;

	private ParticleSystem explosionParticles;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rd = GetComponent<Renderer> ();
		explosionParticles = Instantiate (explosionPrefab).GetComponent<ParticleSystem> ();
		//explosionParticles.gameObject.SetActive (false);
		//rb.velocity = Vector3.forward * speed;

		Color c = rd.material.color;
		c.a = 0;
		rd.material.color = c;

		Destroy (gameObject, 4f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider other) {

		EnemyController enemy = other.GetComponent<EnemyController> ();
		//if (enemy == null)
		//	enemy = other.GetComponent<TempPracticeEnemyController> ();
		if (enemy != null) {			
			enemy.TakeDamage (damage);
		}

		PlayerController player = other.GetComponent<PlayerController> ();
		if (player != null) {			
			player.TakeDamage (damage);
		}

		explosionParticles.transform.position = transform.position;
		//explosionParticles.gameObject.SetActive (true);
		explosionParticles.Play ();
		explosionParticles.GetComponent<AudioSource> ().Play ();
		Destroy (
			explosionParticles.gameObject, 
			Mathf.Max(explosionParticles.duration, explosionParticles.GetComponent<AudioSource> ().clip.length)
		);
		Destroy (gameObject);
	}

	public void SetVelocity (Vector3 v) {
		rb.velocity = v.normalized * speed;
	}

	public void SetDamage (float damage) {
		this.damage = damage;
		//float s = 0.7f * damage;
		//transform.localScale = new Vector3 (s, s, s);
	}
}
