using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DancerController : MonoBehaviour {
	public static DancerController Instance { get; private set; }

	private int movements = 0;
	private GameObject[] markers;
	private int arrow_qty;

	//SOUND
	public AudioSource audio;
	public AudioClip[] tracks;
	public float speed = 0.1f;

	//EFFECTS
	public AudioSource crowdAudio;
	public AudioSource explosionAudio;
	public AudioSource popAudio;

	//Otra cosa
	private Renderer arrow_left;
	private Renderer arrow_up;
	private Renderer arrow_right;
	private bool ready = false;


	//ANIMATIONS
	public GameObject youWin;
	public GameObject youLose;

	public Text start;

	void Awake(){
		audio = GetComponent<AudioSource>();
		start.text = "";
		if (null == Instance) {
			Instance = this;
		} else {
			Debug.LogError("DancerController::Awake - Instance already set. Is there more than one in scene?");
		}
	}

	void Start (){
		audio.clip = tracks[0];
		youWin.SetActive(false);
		youLose.SetActive(false);

		markers = GameObject.FindGameObjectsWithTag("Marker");
		arrow_qty = markers.Length;
		Debug.Log("DancerController::Start - Should have "+arrow_qty+" carts in scene");

		arrow_left = markers[0].GetComponent<Renderer>();
		arrow_up = markers[1].GetComponent<Renderer>();
		arrow_right = markers[2].GetComponent<Renderer>();
	}
	void Update (){
		if (Arrows_Visible() && ready == false){
			ready = true;
			StartCoroutine("ChangeActiveArrow");
		}
	}

	bool Arrows_Visible () {
		if (arrow_left.isVisible && arrow_up.isVisible && arrow_right.isVisible) {
			return true;
		}
		return false;
	}

	IEnumerator ChangeActiveArrow() {
		int act,act1;

		// 3 2 1 START 
		for(int x=3;x>0;x--){
			start.text = x.ToString();
			popAudio.Play();
			yield return new WaitForSeconds(1);
		} start.text = "";

		audio.Play();
		while(audio.isPlaying){
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
	
	public void GameFinish() {
		// El jugador gana si logra mas de 90% de los movimientos
		if(ScoreManager.score >= movements * 90 * (0.01)){
			Debug.Log("DancerController::[YOU WIN] animation");
			youWin.SetActive(true);
			crowdAudio.Play();
			youWin.GetComponent<Animation>().Play("YouWin");
		}else{
			Debug.Log("DancerController::[YOU LOSE] animation");
			youLose.SetActive(true);
			youLose.GetComponent<Animation>().Play("YouLose");
			explosionAudio.Play();
		}
	}
}