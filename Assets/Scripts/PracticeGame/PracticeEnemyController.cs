using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PracticeEnemyController : EnemyController {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(transform.position.z - readyPosition.y) >= 0.1f)
			transform.position = Vector3.MoveTowards (transform.position, readyPosition, 0.07f);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.Euler (0f, 180f, 0f), 10);
	}

	public override void TakeDamage(float amount){
		StartCoroutine (PlayHitAnimation ());
	}

	IEnumerator PlayHitAnimation () {
		Animator a = GetComponent<Animator> ();
		a.SetTrigger ("Hit");
		float s = (float) a.GetCurrentAnimatorClipInfo (0).Length;
		yield return new WaitForSeconds (s);
		a.ResetTrigger ("Hit");
	}
}