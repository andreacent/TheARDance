using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DancerController : MonoBehaviour {
	public static DancerController Instance { get; private set; }

	private int movements = 0;
	private GameObject[] markers;
	public int speed = 3;

	public bool gameHasStarted = false;
	public bool gameIsDone = false;

	private float time_song = 0.95f;
	private float time = 0f;
	public Text gameover;

	void Awake(){
		gameover.text = "";
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
		time = 0;
		while(time < time_song){
			time += Time.deltaTime;
			act = Random.Range(0, 5);
			markers[act].transform.GetComponent<ArrowBehaviors>().active = true;
			Debug.Log("DancerController:: marker "+act+" active");
			yield return new WaitForSeconds(speed);
			markers[act].transform.GetComponent<ArrowBehaviors>().active = false;
			Debug.Log("DancerController:: marker "+act+" desactive");
			movements += 1;
		}
		
		// El jugador gana si logra mas de 90% de los movimientos
		if(ScoreManager.score >= movements * 90 * (0.01)){
			gameover.text = "YOU WIN";
		}else{
			gameover.text = "GAME OVER";
		}
	}
	
	public void GameStart() {
		gameHasStarted = true;
	}
}