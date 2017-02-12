using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DancerController : MonoBehaviour {
	public static DancerController Instance { get; private set; }

	private GameObject[] markers;
	public int speed = 3;

	public bool gameHasStarted = false;
	public bool gameIsDone = false;

	void Awake(){
		if (null == Instance) {
			Instance = this;
		} else {
			Debug.LogError("DancerController::Awake - Instance already set. Is there more than one in scene?");
		}
	}

	void Start (){
		markers = GameObject.FindGameObjectsWithTag("Marker");
		StartCoroutine("ChangeActiveArrow");
	}

	IEnumerator ChangeActiveArrow() {
		int act;
		while(true){
			act = Random.Range(0, 5);
			markers[act].transform.GetComponent<ArrowBehaviors>().active = true;
			Debug.Log("DancerController:: marker "+act+" active");
			yield return new WaitForSeconds(speed);
			markers[act].transform.GetComponent<ArrowBehaviors>().active = false;
			Debug.Log("DancerController:: marker "+act+" desactive");
		}
	}
	
	public void GameStart() {
		gameHasStarted = true;
	}
}