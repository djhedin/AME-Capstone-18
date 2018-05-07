using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour {

	private GameObject ballObject;
	public Vector3 pos;
	// Use this for initialization
	void Start () {
		ballObject = GameObject.Find ("Ball");
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position;

//		if (ballObject.transform.position.z > 10) {
//			if (pos.x < ballObject.transform.position.x - Random.value) {
//				pos.x += 0.15f;
//			}
//			if (pos.y < ballObject.transform.position.y - Random.value) {
//				pos.y += 0.15f;
//			}
//		}
		pos.x = ballObject.transform.position.x;
		pos.y = ballObject.transform.position.y;

		transform.position = pos;
	}
}
