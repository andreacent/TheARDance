using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DancerController : MonoBehaviour {
	public static DancerController Instance { get; private set; }

	public int maxNumberOfCardsInPlay = 2;
	public int cardsNeededForGameToStart = 2;
	public List<GameObject> cardsInPlay = new List<GameObject>();
	private bool ready = false;

	public bool gameHasStarted = false;
	public bool gameIsDone = false;
	private Transform arrow1;
	private Transform arrow2;
	private ArrowBehaviors arrow1behaviors;
	private ArrowBehaviors arrow2behaviors;

	void Awake() {
		if (null == Instance) {
			Instance = this;
		} else {
			Debug.LogError("DancerController::Awake - Instance already set. Is there more than one in scene?");
		}
	}

	//void Start () {
	//}
	
	void Update () {
		Debug.Log("DancerController:: Update - number of cards:" + cardsInPlay.Count);
		CheckIfCards();
		if (ready == true) {
			Debug.Log("DancerController:: Si hay dos cartas");
			ChangeArrowColor();
		}
	}

	// checks if two cards are in view and stores references to their associated character scripts
	// if there are two. If not two cards, reset certain booleans, etc.
	void CheckIfCards() {

		if (cardsInPlay.Count == cardsNeededForGameToStart && ready == false) {
			arrow1 = cardsInPlay[0].transform;//.transform.FindChild("Character");
			arrow2 = cardsInPlay[1].transform;//.transform.FindChild("Character");
			arrow1behaviors = arrow1.GetComponent<ArrowBehaviors>();
			arrow2behaviors = arrow2.GetComponent<ArrowBehaviors>();
			ready = true;
			//StartCoroutine(IntroStart()); NO ENTIENDO ESTA LINEA
		}
		if (cardsInPlay.Count != cardsNeededForGameToStart && ready == true) {
			ready = false;
			gameHasStarted = false;
		}
	}

	public void GameStart() {
		gameHasStarted = true;
	}

	void ChangeArrowColor() {	
		arrow1behaviors.myColor.SetActive(true);
		arrow2behaviors.myColor.SetActive(true);
	}

}