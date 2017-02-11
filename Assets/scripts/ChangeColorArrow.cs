using UnityEngine;
using System.Collections;

public class ChangeColorArrow : MonoBehaviour {

	public Material[] material;
	Renderer rend;
	
	void Start () {
		rend = transform.parent.Find("arrow").GetComponent<Renderer>();
		rend.enabled = true;
		rend.sharedMaterial = material[0];
	}

	void Update () {
		ChangeColor();
	}
	
	void OnDisable() {
		rend.sharedMaterial = material[0];
	}
	
	void ChangeColor() {
		rend.sharedMaterial = material[1];
	}	
}