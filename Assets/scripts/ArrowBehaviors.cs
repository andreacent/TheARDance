using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviors : MonoBehaviour {
	private enum PopupMode { None = 0, Started = 1, InProgress = 2, Ending = 3 };
	private static Camera arCamera = null;

	public bool active = false;
	public GameObject myColor;

	// Use this for initialization
	void Start () {
		if (null == ArrowBehaviors.arCamera) {
			ArrowBehaviors.arCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
	}
	
	void OnDisable() {
		if (null == DancerController.Instance) {
			Debug.LogError("ArrowHolder::OnDisable - DancerController.Instance not set. Is there one in the scene?");
			return;
		}
		if (active){
			DancerController.Instance.score += 1;
			active = false;
		}
	}
}
