using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public float maxHealth = 100f;
	public Slider healthSlider;
	public PlayerController player;
	public Rigidbody fireballPrefab;
	public Vector3 readyPosition = new Vector3 (0f, -4.5f, -1f);

	public GameObject explosionPrefab;

	private float health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		// TODO bind health slider with the thing
		healthSlider.value = health * 100 / maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(transform.position.z - readyPosition.y) >= 0.1f)
			transform.position = Vector3.MoveTowards (transform.position, readyPosition, 0.07f);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.Euler (0f, 180f, 0f), 10);
	}

	public void TakeDamage(float amount){
		health -= amount;
		healthSlider.value = health * 100 / maxHealth;
		if (health <= 0) {
			OnDeath ();
		} else {
			StartCoroutine (PlayHitAnimation ());
		}
	}

	IEnumerator PlayHitAnimation () {
		Animator a = GetComponent<Animator> ();
		a.SetTrigger ("Hit");
		float s = (float) a.GetCurrentAnimatorClipInfo (0).Length;
		yield return new WaitForSeconds (s);
		a.ResetTrigger ("Hit");
	}

	public void Fire () {
		Vector3 fireballSpawnPoint = transform.position + new Vector3(3, 3, 0) + Vector3.back * 0f;
		Rigidbody fireballRB = Instantiate (
			fireballPrefab,
			fireballSpawnPoint,
			Quaternion.identity
		) as Rigidbody;
		fireballRB.velocity = (player.gameObject.transform.position - fireballSpawnPoint).normalized * 30f;
	}

	public bool IsDead () {
		return health <= 0;
	}

	public void OnDeath () {
		Animator a = GetComponent<Animator> ();
		a.SetTrigger ("DeathTrigger");
		ParticleSystem explosionParticles = Instantiate (explosionPrefab).GetComponent<ParticleSystem> ();
		explosionParticles.Play ();
		Destroy (explosionParticles.gameObject, explosionParticles.duration);
		Destroy (gameObject, 0f);
	}

	public void SetPlayer (PlayerController player) {
		this.player = player;
	}

	public void SetHealthDisplay (HealthController health) {
		healthSlider = health.GetComponent<Slider> ();
	}
}