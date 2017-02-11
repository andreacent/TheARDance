using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviors : MonoBehaviour {
	private enum PopupMode { None = 0, Started = 1, InProgress = 2, Ending = 3 };

	private static Camera arCamera = null;

	// Gloves Punching  	CAMBIAR POR ARROW
	//private float punchTimer;
	//private float punchStartTime;
	//private float maxPunchTime = .65f;
	//private bool punchTimerHasStarted = false;
	//private bool glovesShouldRetract = true;
	//private Transform punchingGlove;
	//private Vector3 punchingGloveStartPosition;

	public GameObject myColor; 

	// Use this for initialization
	void Start () {
		if (null == ArrowBehaviors.arCamera) {
			ArrowBehaviors.arCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		}
	}
	
	void OnEnable() {
		ResetMe();

		if (DancerController.Instance.cardsInPlay.Count < DancerController.Instance.maxNumberOfCardsInPlay
		    && !DancerController.Instance.cardsInPlay.Contains(gameObject)) {
			DancerController.Instance.cardsInPlay.Add(gameObject);
		}
	}

	void OnDisable() {
		if (null == DancerController.Instance) {
			Debug.LogError("ArrowHolder::OnDisable - DancerController.Instance not set. Is there one in the scene?");
			return;
		}
		
		DancerController.Instance.cardsInPlay.Remove(gameObject);
	}

	void ResetMe() {
		if (null == DancerController.Instance) {
			Debug.LogError("ArrowBehaviors::ResetMe - DancerController.Instance not set. Is there one in the scene?");
			return;
		}

		myColor.SetActive(false);
	}
}
