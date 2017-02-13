using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviors : MonoBehaviour {
	private static Camera arCamera = null;

	//public GameObject myColor;

	public bool active = false;
	public Material[] material;
	Renderer rend;

	void Start () {
		if (null == ArrowBehaviors.arCamera) {
			ArrowBehaviors.arCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		rend.sharedMaterial = material[0];
	}
	
	void Update () {
		if (active) rend.sharedMaterial = material[1];
		else rend.sharedMaterial = material[0];
	}	

	void OnDisable() {
		if (null == DancerController.Instance) {
			Debug.LogError("ArrowHolder::OnDisable - DancerController.Instance not set. Is there one in the scene?");
			return;
		}
		if (active){
			ScoreManager.score += 1;
			active = false;
		}
	}
}
