using UnityEngine;

public class CameraController : MonoBehaviour {
	public float panSpeed = 20f;

	private bool gyroEnabled;

	private Gyroscope gyro;
	private GameObject cameraContainer;
	private Quaternion rot;

	public AndroidJavaClass jc;
	public string intentExtra = "";
	public float[] speeds = { 0f, 0f, 0f };

	public Vector2 panLimit;

//	//Helper function to parse intentExtra into floats and assign to speedList
//	    void ParseFile()
//	    {
//	        string text = intentExtra;
//	        char[] separators = { ',' };
//	        string[] strValues = text.Split(separators);
//	        int i = 0;
//	        foreach (string str in strValues)
//	        {
//	            float val = 0f;
//	            if (float.TryParse(str, out val)) {
//	                speeds[i] = val;
//	            }
//	            i++;
//	        }
//	
//	    }

	void Start() {
		cameraContainer = new GameObject ("Camera Container");
		cameraContainer.transform.position = transform.position;

		transform.SetParent (cameraContainer.transform);

		gyroEnabled = EnableGyro ();
//
//		// Access the android java receiver we made
//		jc = new AndroidJavaClass("com.devynhedin.receiver.MyReceiver");
//		// We call our java class function to create our MyReceiver java object
//		jc.CallStatic("createInstance");
	}

	private bool EnableGyro() {
		if (SystemInfo.supportsGyroscope) {
			gyro = Input.gyro;
			gyro.enabled = true;

			cameraContainer.transform.rotation = Quaternion.Euler (90f, 90f, 0f);
			rot = new Quaternion (0, 0, 1, 0);

			return true;
		}
		return false;
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if (Input.GetKey("w")) {
			pos.y += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("s")) {
			pos.y -= panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("d")) {
			pos.x += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("a")) {
			pos.x -= panSpeed * Time.deltaTime;
		}
//		ParseFile();
//		pos.x = speeds [0];
//		pos.y = speeds [1];
//		pos.z = speeds [2];

		transform.position = pos;
		if (gyroEnabled) {
			transform.localRotation = gyro.attitude * rot;
		}
	}
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class CameraController : MonoBehaviour {
//    //public float panSpeed = 200f;

//
//    
//
//    // Use this for initialization
//    void Start()
//    {
//        
//    }
//
//    // Update is called once per frame
//    void Update () {
//        intentExtra = jc.GetStatic<string>("delta");
//        Vector3 pos = transform.position;
//        Quaternion rot = transform.rotation;
//        
//        //intentExtra = jc.GetStatic<string>("delta");
//        pos.x += speeds[0] * Time.deltaTime;
//        pos.y += speeds[1] * Time.deltaTime;
//        pos.z += speeds[2] * Time.deltaTime;
//
//        transform.position = pos;
//	}
//}
