using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
		
	void LateUpdate () {
		GameObject theCamera = GameObject.Find("UI Camera");
		
		transform.position = theCamera.transform.position;
		transform.rotation = theCamera.transform.rotation;
	}
}
