using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
	    transform.rotation = Quaternion.Euler(
	        Camera.main.transform.rotation.eulerAngles.x,
	        Camera.main.transform.rotation.eulerAngles.y,
	        0);
	}
}
