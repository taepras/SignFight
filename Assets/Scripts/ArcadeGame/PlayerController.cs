using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float maxHealth = 1000f;
	public Slider healthSlider;
	public EnemyController enemy;
	public Rigidbody fireballPrefab;
	public Image dangerOverlay;

	private float health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
		if (dangerOverlay != null)
			dangerOverlay.color = new Color (1f, 1f, 1f, 0f);
	}

	// Update is called once per frame
	void Update () {
		if (dangerOverlay != null) {
			if (health <= 0.2 * maxHealth) {
				dangerOverlay.color = new Color (1f, 1f, 1f, 1f);
			} else if (health <= 0.3f * maxHealth) {
				dangerOverlay.color = new Color (1f, 1f, 1f, 0.3f);
			} else {
				dangerOverlay.color = new Color (1f, 1f, 1f, 0f);
			}
		}
	}

	public void TakeDamage(float amount){
		if (IsDead ()) {
			return;
		}
		health -= amount;
		health = Mathf.Max (0, health);
		healthSlider.value = health * 100 / maxHealth;
	}

	public void Fire (float multiplier) {
		Vector3 spawnPoint = new Vector3 (Random.Range (-4, 4), Random.Range (-4, 4), -5);
		Rigidbody fireballRB = Instantiate (
			fireballPrefab,
			spawnPoint,
			Quaternion.identity
		) as Rigidbody;
		FireBallController fb = fireballRB.GetComponent<FireBallController> ();
		fb.SetDamage(multiplier * fb.damage);
		fireballRB.velocity = (enemy.gameObject.transform.position + Vector3.up * 4 - spawnPoint).normalized * 30f;
	}

	public void SetEnemy (EnemyController g) {
		enemy = g;
	}

	public bool IsDead () {
		return health <= 0;
	}

	public void IncreaseHealth (float amount) {
		health += amount;
		if (health > maxHealth)
			health = maxHealth;
		healthSlider.value = health * 100 / maxHealth;
	}

	public float GetHealthPercentage () {
		return health / maxHealth * 100;
	}
}
