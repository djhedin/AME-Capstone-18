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
		if (ballObject.transform.position.z < -26) {
			aiScore += 1;
			ballObject.transform.position = new Vector3 (0f, 0f, 0f);
			bController.startGame ();
		}
		if (ballObject.transform.position.z > 26) {
			playerScore += 1;
			ballObject.transform.position = new Vector3 (0f, 10f, 0f);
			bController.startGame ();
		}
	}
}
