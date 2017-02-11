using UnityEngine;
using System.Collections;

public class ChangeColorArrow : MonoBehaviour {

	public Material[] material;
	Renderer rend;
	private GameObject theCamera = null;

	void OnEnable () {
		FindCamera();
	}

	void FindCamera () {
		theCamera = GameObject.FindWithTag("MainCamera");
	}
	
	void Start () {
		rend = transform.parent.Find("arrow").GetComponent<Renderer>();
		rend.enabled = true;
		rend.sharedMaterial = material[0];
	}

	void Update () {
		ChangeColor();
	}
	
	void ChangeColor() {
		if(theCamera == null) {
			FindCamera();
		}
		rend.sharedMaterial = material[1];
	}	
}