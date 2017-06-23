using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldUI : MonoBehaviour {

	/*Manages UI and player interface options in the open world.*/

	private Text HEALTH;
	private GameObject[] DIALOGUES;
	private CameraParallax CONTROLS;

	public void initDialogueOne() {
		DIALOGUES[0].SetActive (true);
		CONTROLS.enabled = false;
	}

	public void exitDialogue() {
		foreach (GameObject dialogue in DIALOGUES) {
			dialogue.SetActive (false);
		}
		CONTROLS.enabled = true;
	}

	// Use this for initialization
	void Awake () {
		CONTROLS = GameObject.Find ("Main Camera").GetComponent<CameraParallax> ();
		DIALOGUES = GameObject.FindGameObjectsWithTag("Dialogue");
		DIALOGUES[0].SetActive (false);

		//HEALTH = GameObject.FindGameObjectWithTag ("health").GetComponent<Text> ();
		//HEALTH.text = System.Convert.ToString (Teamstate.teamstate.player.Health);
	}
	
	// Update is called once per frame
	void Update () {
		//HEALTH.text = "HP:" + System.Convert.ToString(Teamstate.teamstate.player.Health);
	}
}
