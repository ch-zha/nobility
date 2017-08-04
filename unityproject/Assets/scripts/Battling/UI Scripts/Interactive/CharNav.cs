using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharNav : MonoBehaviour {

	public CharSelect allyOne;
	public CharSelect allyTwo;
	public CharSelect allyThree;

	public CharSelect currentAlly {get; set;}
	private GameObject lastSelected { get; set;}

	public void goToNext() {
		if (currentAlly == allyOne) {
			currentAlly = allyTwo;
		} else if (currentAlly == allyTwo) {
			currentAlly = allyThree;
		} else if (currentAlly == allyThree) {
			currentAlly = allyOne;
		} else {
			Debug.Log ("Error: Current Ally Not Found");
		}

		currentAlly.selectchar ();
	}

	public void goToLast() {
		if (currentAlly == allyOne) {
			currentAlly = allyThree;
		} else if (currentAlly == allyTwo) {
			currentAlly = allyOne;
		} else if (currentAlly == allyThree) {
			currentAlly = allyTwo;
		}

		currentAlly.selectchar ();
	}

	/*RUNTIME*/

	// Use this for initialization
	void Start () {
		currentAlly = allyOne;
		allyOne.selectchar ();
	}
	
	// Update is called once per frame
	void Update () {
		if (EventSystem.current.currentSelectedGameObject != null) {
			lastSelected = EventSystem.current.currentSelectedGameObject;
		} else {
			EventSystem.current.SetSelectedGameObject (lastSelected);
		}

		if (! CharSelect.coroutineOneRunning && ! CharSelect.coroutineTwoRunning) {
			if (Input.GetKeyDown ("a")) {
				currentAlly.closeMenu ();
				goToLast ();
			}

			if (Input.GetKeyDown ("d")) {
				currentAlly.closeMenu ();
				goToNext ();
			}
		}
	}
}
