using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    //public float panSpeed = 200f;
    public AndroidJavaClass jc;
    public string intentExtra = "";
    public float[] speeds = { 0f, 0f, 0f, 0f, 0f, 0f };

    //Helper function to parse intentExtra into floats and assign to speedList
    void ParseFile()
    {
        string text = intentExtra;
        char[] separators = { ',' };
        string[] strValues = text.Split(separators);
        int i = 0;
        foreach (string str in strValues)
        {
            float val = 0f;
            if (float.TryParse(str, out val)) {
                speeds[i] = val;
            }
            i++;
        }

    }

    // Use this for initialization
    void Start()
    {
        // Access the android java receiver we made
        jc = new AndroidJavaClass("com.devynhedin.receiver.MyReceiver");
        // We call our java class function to create our MyReceiver java object
        jc.CallStatic("createInstance");
    }

    // Update is called once per frame
    void Update () {
        intentExtra = jc.GetStatic<string>("delta");
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        ParseFile();
        //intentExtra = jc.GetStatic<string>("delta");
        pos.x += speeds[0] * Time.deltaTime;
        pos.y += speeds[1] * Time.deltaTime;
        pos.z += speeds[2] * Time.deltaTime;

        transform.position = pos;
	}
}
