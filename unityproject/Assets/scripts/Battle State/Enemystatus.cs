using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemystatus : MonoBehaviour {
	/*Initiates enemies & tracks their health and 
	 * cooldowns throughout the battle.*/

	public Participant enemyOne;

	public bool allDead = false;

	// Use this for initialization
	void Start () {
		enemyOne = new Participant (new Lonk());
	}

	void checkDead() {
		if (enemyOne.dead == true) { allDead = true;}
	}
	
	// Update is called once per frame
	void Update () {
		checkDead ();
	}
}
