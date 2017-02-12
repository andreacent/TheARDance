using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DancerController : MonoBehaviour {
	public static DancerController Instance { get; private set; }

	private GameObject[] markers;
	public int score = 0;
	public int speed = 2;

	public bool gameHasStarted = false;
	public bool gameIsDone = false;

	void Awake() {
		if (null == Instance) {
			Instance = this;
		} else {
			Debug.LogError("DancerController::Awake - Instance already set. Is there more than one in scene?");
		}
	}

	void Start () {
		markers = GameObject.FindGameObjectsWithTag("Marker");
		StartCoroutine("ChangeActiveArrow");
	}

	IEnumerator ChangeActiveArrow() {
		while(true){
			for (int x = 0; x < 5; x++) {
	        	yield return new WaitForSeconds(speed);
        		Debug.Log("DancerController:: marker "+x+" active");

        		//cambiarlo cuando este random
        		if(x>0) markers[x-1].transform.GetComponent<ArrowBehaviors>().active = false;
        		else markers[4].transform.GetComponent<ArrowBehaviors>().active = false;
        		
	        	markers[x].transform.GetComponent<ArrowBehaviors>().active = true;
	     	}
	    }
	}
	
	public void GameStart() {
		gameHasStarted = true;
	}
}