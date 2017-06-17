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

	/*Skills*/
	//change to fetch functions later
	private Skill snartscreen;
	private int snartscreenToUse;

	private Skill sidebern;
	private int sidebernToUse;

	void Awake() {
		//collect character data
		//find GUI elements
		PLAYERHEALTH = GameObject.FindGameObjectWithTag("health").GetComponent<Text>(); //change to tag handling
		TURNCOUNT = GameObject.Find("turn").GetComponent<Text>(); //change to tag handling
	}

	void Start() {
		//initialize turn counters & stuff
		turn = 0;

		/*TESTING ONLY VARS*/
		snartscreen = new Skill(3);
		snartscreenToUse = snartscreen.Cooldown;

		sidebern = new Skill (2);
		sidebernToUse = sidebern.Cooldown;
	}

/*INTERFACE MANAGEMENT*/
	private void displayHealth() {
		//add more characters later
		PLAYERHEALTH.text = "HP:" + System.Convert.ToString(Teamstate.teamstate.player.Health);
	}

	private void displayTurn() {
		TURNCOUNT.text = "Turn:" + System.Convert.ToString (turn);
	}

/*BATTLE STRUCTURE*/
	/*Increment Turn*/
	public void incrementTurn() {turn++;}

	/*Reduce CD of all skills*/
	private void reduceAllCD() {
		if (snartscreenToUse != 0) { snartscreenToUse--; }
		if (sidebernToUse != 0) { sidebernToUse--;}
	}

/*SKILLS*/
//move skill constructors to character scripts once mechanics set
//replace with search function to find appropriate skills in character prefab

	public void useSnartscreen() {
		if (snartscreenToUse == 0) {
			reduceAllCD();
			snartscreen.reduceHealth (this);
			snartscreenToUse = snartscreen.Cooldown;
		}
	}

	public void useSideBern() {
		if (sidebernToUse == 0) {
			reduceAllCD();
			sidebern.increaseHealth (this);
			sidebernToUse = sidebern.Cooldown;
		}
	}

	public void emptySkill() {
		reduceAllCD();
		incrementTurn ();
	}

/*GAME FUNCTIONS*/
	public void endBattle() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}

/*UPDATE*/
	void Update() {
		displayHealth ();
		displayTurn ();

		//cd displays - make custom function when done writing
		GameObject.Find ("snartscreentext").GetComponent<Text> ().text = "Snartscreen: CD " + System.Convert.ToString (snartscreenToUse);
		GameObject.Find ("sideberntext").GetComponent<Text> ().text = "SideBern: CD " + System.Convert.ToString (sidebernToUse);
	}
}