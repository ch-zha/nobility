using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerstatus : MonoBehaviour {
	/*Initiates player team and handles their actions and cds throughout
	 * the battle.*/

	public Participant teamOne;

	public bool allDead = false;

	// Use this for initialization
	void Start () {
		teamOne = new Participant (new Windblade(1));
	}

	void checkDead() {
		if (teamOne.dead == true) { allDead = true;}
	}

	// Update is called once per frame
	void Update () {
		checkDead();
	}
}
