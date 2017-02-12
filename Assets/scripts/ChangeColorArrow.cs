using UnityEngine;
using System.Collections;

public class ChangeColorArrow : MonoBehaviour {

	public Material[] material;
	Renderer rend;

	private Transform arrow;
	private ArrowBehaviors arrowbehaviors;
	
	void Start () {
		arrow = transform.parent.Find("arrow");
		arrowbehaviors = arrow.GetComponent<ArrowBehaviors>();
		rend = arrow.GetComponent<Renderer>();
		rend.enabled = true;
		rend.sharedMaterial = material[0];
	}

	void Update () {
		if (arrowbehaviors.active){
			rend.sharedMaterial = material[1];
		}else{
			rend.sharedMaterial = material[0];
		}
	}	
}