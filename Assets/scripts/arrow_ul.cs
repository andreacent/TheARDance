using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow_ul : MonoBehaviour {

	public Color color = Color.black;
	public int count_ul;

	// Use this for initialization
	void Start () {
		StartCoroutine(ChangeColor());
		count_ul = 0;
	}
		

	// Update is called once per frame
	//void Update () { 
		
	//	ChangeColor ();
	//}

	IEnumerator ChangeColor()
	{
		color = gameObject.GetComponent<Renderer> ().material.color;
		while (true) {
			yield return new WaitForSeconds(4);
			gameObject.GetComponent<Renderer>().material.color = Color.red;
			if(gameObject.GetComponent<Renderer>().material.color == Color.red){
				count_ul += 1;
			}
			Debug.Log("time started");
			yield return new WaitForSeconds(2);
			Debug.Log("time ended");
			gameObject.GetComponent<Renderer> ().material.color = color;
			yield return new WaitForSeconds(4);
		}
	}
		
}
