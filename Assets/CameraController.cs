using UnityEngine;

public class CameraController : MonoBehaviour {
	public float panSpeed = 20f;

	private bool gyroEnabled;

	private Gyroscope gyro;
	private GameObject cameraContainer;
	private Quaternion rot;

	public Vector2 panLimit;   


	/////////////////////////////////////////////////////////////////////////
	private GameObject ballObject;
	/////////////////////////////////////////////////////////////////////////

	void Start() {
		cameraContainer = new GameObject ("Camera Container");
		cameraContainer.transform.position = transform.position;

		transform.SetParent (cameraContainer.transform);

		gyroEnabled = EnableGyro ();

		ballObject = GameObject.Find ("Ball");
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
//		string btData = jc.Get<string> ("data");
		Vector3 pos = transform.position;
		if (Input.GetKey("w") && pos.y <= 11.5f) {
			pos.y += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("s") && pos.y >= 2.5f) {
			pos.y -= panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("d") && pos.x <= 12.5f) {
			pos.x += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("a") && pos.x >= -12.5f) {
			pos.x -= panSpeed * Time.deltaTime;
		}

		///////////////////////////////////////////////////////////////////////
//		pos.x = ballObject.transform.position.x;
//		pos.y = ballObject.transform.position.y;
		///////////////////////////////////////////////////////////////////////

		cameraContainer.transform.position = pos;
		if (gyroEnabled) {
			transform.localRotation = gyro.attitude * rot;
		}
	}
}