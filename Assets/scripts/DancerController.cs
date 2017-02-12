using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DancerController : MonoBehaviour {
	public static DancerController Instance { get; private set; }

	private int movements = 0;
	private GameObject[] markers;
	public int arrow_qty = 3;
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
		int act,act1;
		time = 0;
		while(time < time_song){
			time += Time.deltaTime;
			act = Random.Range(0, arrow_qty);
			// active arrow with index act 
			ActiveArrow(act);
			yield return new WaitForSeconds(speed);
			DesactiveArrow(act);

			//Random 10% for aditional arrow
			if ( 0 == Random.Range(0, 10) ){
				do{ act1 = Random.Range(0, arrow_qty); } while (act == act1);
				// active arrow with index act1
				ActiveArrow(act1);
				yield return new WaitForSeconds(speed);
				DesactiveArrow(act1);
			}
		}
		
		// El jugador gana si logra mas de 90% de los movimientos
		if(ScoreManager.score >= movements * 90 * (0.01)){
			gameover.text = "YOU WIN";
		}else{
			gameover.text = "GAME OVER";
		}
	}

	void ActiveArrow(int act){
		markers[act].transform.GetComponent<ArrowBehaviors>().active = true;
		Debug.Log("DancerController:: marker "+act+" active");
		movements += 1;
	}

	void DesactiveArrow(int act){
		markers[act].transform.GetComponent<ArrowBehaviors>().active = false;
		Debug.Log("DancerController:: marker "+act+" desactive");
	}
	
	public void GameStart() {
		gameHasStarted = true;
	}
}