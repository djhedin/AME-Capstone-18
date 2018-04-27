using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepScore : MonoBehaviour {

	public int playerScore;
	public int aiScore;
	private GameObject ballObject;
	public BallController bController;

	// Use this for initialization
	void Start () {
		playerScore = 0;
		aiScore = 0;
		ballObject = GameObject.Find ("Ball");

	}
	
	void Update () {
		GetComponent<TextMesh> ().text = "AI Score: " + aiScore.ToString() + "\nPlayer Score: " + playerScore.ToString();
		if (ballObject.transform.position.z < -1) {
			aiScore += 1;
			ballObject.transform.position = new Vector3 (0f, 1.5f, 10f);
			bController.startGame ();
		}
		if (ballObject.transform.position.z > 19) {
			playerScore += 1;
			ballObject.transform.position = new Vector3 (0f, 1.5f, 10f);
			bController.startGame ();
		}
	}
}
