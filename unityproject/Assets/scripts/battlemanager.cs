using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battlemanager : MonoBehaviour {

	/*UI Vars*/
	private Text PLAYERHEALTH;
	private Text TURNCOUNT;

	/*Mechanic Vars*/
	private int turn;

	void Awake() {
		//collect character data
		//find GUI elements
		PLAYERHEALTH = GameObject.FindGameObjectWithTag("health").GetComponent<Text>(); //change to tag handling
		TURNCOUNT = GameObject.Find("turn").GetComponent<Text>(); //change to tag handling
	}

	void Start() {
		//initialize turn counters & stuff
		turn = 0;
	}

/*INTERFACE MANAGEMENT*/
	void displayHealth() {
		//add more characters later
		PLAYERHEALTH.text = "HP:" + System.Convert.ToString(Teamstate.teamstate.player.Health);
	}

	void displayTurn() {
		TURNCOUNT.text = "Turn:" + System.Convert.ToString (turn);
	}

/*BATTLE STRUCTURE*/
	/*Increment Turn*/
	public void incrementTurn() {turn++;}

	/*Character CDs*/
	//switch to array when multiple skills implemented?

/*BATTLE ACTIONS*/

	/*PLAYER SIDE*/
	public void reduceHealth() {
		//add handling for other characters later
		Teamstate.teamstate.changeHealth(-20);
		incrementTurn ();
		if (Teamstate.teamstate.player.Health == 0) {
			endBattle ();
		}
	}

	public void increaseHealth() {
		Teamstate.teamstate.changeHealth (20);
		incrementTurn ();
	}

	/*ENEMY SIDE*/
	//import enemy stats, functions, etc.

/*GAME FUNCTIONS*/
	public void endBattle() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}

/*UPDATE*/
	void Update() {
		displayHealth ();
		displayTurn ();
	}
}