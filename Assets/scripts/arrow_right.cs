using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_right : MonoBehaviour {

	public Color color = Color.black;

	// Use this for initialization
	void Start () {
		StartCoroutine(ChangeColor());
	}
	
	IEnumerator ChangeColor()
	{
		color = gameObject.GetComponent<Renderer> ().material.color;
		while (true) {
			yield return new WaitForSeconds(3);
			gameObject.GetComponent<Renderer>().material.color = Color.red;
			Debug.Log("time started");
			yield return new WaitForSeconds(2);
			Debug.Log("time ended");
			gameObject.GetComponent<Renderer> ().material.color = color;
			yield return new WaitForSeconds(3);
		}
	}
}
