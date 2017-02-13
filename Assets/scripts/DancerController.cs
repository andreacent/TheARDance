using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DancerController : MonoBehaviour {
	public static DancerController Instance { get; private set; }

	private int movements = 0;
	private GameObject[] markers;
	private int arrow_qty;
	public float speed = 1.0f;

	//ANIMATIONS
	public GameObject youWin;
	public GameObject youLose;

	public bool gameHasStarted = false;
	public bool gameIsDone = false;

	private float time_song = 0.1f;
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
		youWin.SetActive(false);
		youLose.SetActive(false);

		markers = GameObject.FindGameObjectsWithTag("Marker");
		arrow_qty = markers.Length;
		Debug.Log("DancerController::Start - Should have "+arrow_qty+" carts in scene");

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
			//Random 10% for aditional arrow
			if ( 0 == Random.Range(0, 10) ){
				do{ act1 = Random.Range(0, arrow_qty); } while (act == act1);
				// active arrow with index act1
				ActiveArrow(act1);
			yield return new WaitForSeconds(speed);
			DesactiveArrow(act1);
			}else{

			yield return new WaitForSeconds(speed);
			}
			// active arrow with index act 
			DesactiveArrow(act);
		}
		GameFinish();
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

	public void GameFinish() {
		gameIsDone = true;
		// El jugador gana si logra mas de 90% de los movimientos
		if(ScoreManager.score >= movements * 90 * (0.01)){
			Debug.Log("DancerController::[YOU WIN] animation");
			youWin.SetActive(true);
			youWin.GetComponent<Animation>().Play("YouWin");
		}else{
			Debug.Log("DancerController::[YOU LOSE] animation");
			youLose.SetActive(true);
			youLose.GetComponent<Animation>().Play("YouLose");
		}
	}
}