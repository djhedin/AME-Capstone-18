using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {
	public Vector3 initialVelocity;

	private Rigidbody rb;
	public float speed = 25f;
	void Awake() {
		float sz = Random.Range(0, 2) == 0 ? -1 : 1;
		float sy = Random.Range(0, 2) == 0 ? -1 : 1;

		GetComponent<Rigidbody> ().velocity = new Vector3 (0f, speed * sy, speed * sz);;

//		rb.velocity = new Vector3 (speed * sx, speed * sy, 0f);
		rb.isKinematic = false;
//		rb.AddForce(initialVelocity, ForceMode.Impulse);
	}
}
