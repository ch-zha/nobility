using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battlemanager : MonoBehaviour {

	private Text PLAYERHEALTH;

	void Awake() {
		//collect character data
		//find GUI elements
		PLAYERHEALTH = GameObject.FindGameObjectWithTag("health").GetComponent<Text>(); //change to find by tag later
	}

	void Start() {
		//initialize turn counters & stuff
	}

/*INTERFACE MANAGEMENT*/
	void displayHealth() {
		//add more characters later
		PLAYERHEALTH.text = System.Convert.ToString(Teamstate.teamstate.getHealth(1));
	}

/*BATTLE MECHANICS*/
	public void reduceHealth() {
		//add handling for other characters later
		Teamstate.teamstate.changeHealth(-20);
	}

	public void increaseHealth() {
		Teamstate.teamstate.changeHealth (20);
	}

/*GAME FUNCTIONS*/
	public void endBattle() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}

/*UPDATE*/
	void Update() {
		displayHealth ();
	}
}