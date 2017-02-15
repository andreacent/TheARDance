using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DancerController : MonoBehaviour {
	public static DancerController Instance { get; private set; }

	private int movements = 0;
	private GameObject[] markers;
	private GameObject[] songs;
	private int arrow_qty;

	//SOUND
	public AudioSource audio;
	public AudioClip[] tracks;
	public float speed;
	private int song = -1;

	//EFFECTS
	public AudioSource crowdAudio;
	public AudioSource explosionAudio;
	public AudioSource popAudio;

	//Otra cosa
	private Renderer arrow_left;
	private Renderer arrow_up;
	private Renderer arrow_right;
	private bool ready = false;
	private float time;


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
		youWin.SetActive(false);
		youLose.SetActive(false);
		audio.clip = tracks[0];

		markers = GameObject.FindGameObjectsWithTag("Marker");
		songs = GameObject.FindGameObjectsWithTag("Song");
		arrow_qty = markers.Length;
		Debug.Log("DancerController::Start - Should have "+arrow_qty+" carts in scene");

		arrow_left = markers[0].GetComponent<Renderer>();
		arrow_up = markers[1].GetComponent<Renderer>();
		arrow_right = markers[2].GetComponent<Renderer>();
	}
	void Update (){
		if (Arrows_Visible() && ready == false){
			audio.Stop();
			ready = true;
			StartCoroutine("ChangeActiveArrow");
		}
		if (Song_Visible(0) && ready == false && song != 0) {
			PreviewSong(0);
			}
		else if (Song_Visible(1) && ready == false && song != 1) {
			PreviewSong(1);
		}
	}

	void PreviewSong(int i){
		audio.Stop();
		song = i;
		audio.clip = tracks[i];
		audio.time = audio.clip.length * 0.21f;
		audio.Play();

	}

	bool Song_Visible (int i) {
		if (songs[i].GetComponent<Renderer>().isVisible) {
			return true;
		}
		return false;
	}

	bool Arrows_Visible () {
		if (arrow_left.isVisible && arrow_up.isVisible && arrow_right.isVisible) {
			return true;
		}
		return false;
	}

	IEnumerator ChangeActiveArrow() {
		int act,act1;
		time = audio.clip.length * 0.5f;
		speed = 1.5f;
		audio.time = 0;
		// 3 2 1 START 
		for(int x=3;x>0;x--){
			start.text = x.ToString();
			popAudio.Play();
			yield return new WaitForSeconds(1);
		} start.text = "";

		audio.Play();
		while(audio.isPlaying){
			if (audio.time > time) speed = 1;
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