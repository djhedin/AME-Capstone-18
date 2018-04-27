using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

	public AndroidJavaClass jc;
	public TextMesh textObject;
	public string deltaString;

	public Vector3 pos;

	public Vector3 origin;

	// Use this for initialization
	void Start () {
//		origin = new Vector3 (3f, 4.53f, -18f);

		origin = new Vector3 (0f, 0f, 0f);
		jc = new AndroidJavaClass ("com.devynhedin.receiver.MyReceiver");
		jc.CallStatic ("createInstance");
		textObject = GameObject.Find("BluetoothTextTest").GetComponent<TextMesh>();
	}

	// Update is called once per frame
	void Update () {
		deltaString = jc.GetStatic<string> ("delta");
		textObject.text = deltaString;

		string[] arr = deltaString.Split('\t');

		GameObject.Find ("Coordinate-1").GetComponent<TextMesh> ().text = arr [3] + " | " + (double.Parse(arr[3]) / double.Parse(arr[6]));
		GameObject.Find ("Coordinate-2").GetComponent<TextMesh> ().text = arr [4] + " | " + (double.Parse(arr[4]) / double.Parse(arr[6]));
		GameObject.Find ("Coordinate-3").GetComponent<TextMesh> ().text = arr [5] + " | " + (double.Parse(arr[5]) / double.Parse(arr[6]));
		GameObject.Find ("Coordinate-4").GetComponent<TextMesh> ().text = arr [6];

		double coordinate_1 = double.Parse (arr [3]);
		double coordinate_2 = double.Parse (arr [4]);
		double coordinate_3 = double.Parse (arr [5]);
		double coordinate_4 = double.Parse (arr [6]);

		pos = GameObject.Find ("Main Camera").transform.position;

		if (pos.x < GameObject.Find("Right Wall").transform.position.x && pos.x > GameObject.Find("Left Wall").transform.position.x) {
			pos.x = (float)(coordinate_1 / coordinate_4) + origin.x;
		}
		if (pos.y < GameObject.Find("Ceiling").transform.position.y && pos.y > GameObject.Find("Floor").transform.position.y) {
			pos.y = (float)(coordinate_2 / coordinate_4) + origin.y;
		}

//		pos.x = (float)(coordinate_1 / coordinate_4) + origin.x;
//		pos.y = (float)(coordinate_2 / coordinate_4) + origin.y;
		pos.z = (float)(coordinate_3 / coordinate_4) + origin.z;

		GameObject.Find ("Main Camera").transform.position = pos;

	}
}
