using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battlemanager : MonoBehaviour {

	/*Battlemanager manages the battle state machine and
	 * any variables or functions specific to the battle
	 * instance.*/

	public enum BattleState {
		START,
		PLAYERCHOICE,
		PLAYERANIMATE,
		ENEMYCHOICE,
		ENEMYANIMATE,
		DIALOGUE,
		LOSE,
		WIN
	} 

	public BattleState currentState;
	public int turn;

	private BattleLoad LOAD;
	private BattleUI DISPLAY;

	/*NOTE: TRANSFER ALL TEST VARIABLES TO PROPER CLASSES WHEN BUILT*/

	/*Skills*/
	//change to fetch functions later
	private Skill snartscreen;
	private int snartscreenToUse;

	private Skill sidebern;
	private int sidebernToUse;

	void Start() {
		//initialize turn counters & stuff
		turn = 0;
		currentState = BattleState.START;

		LOAD = this.gameObject.GetComponent<BattleLoad> ();
		DISPLAY = this.gameObject.GetComponent<BattleUI> ();

		/*TESTING ONLY VARS*/
		snartscreen = new Skill(3);
		snartscreenToUse = snartscreen.Cooldown;

		sidebern = new Skill (2);
		sidebernToUse = sidebern.Cooldown;
	}

/*INTERFACE MANAGEMENT*/
//move to UI class later

/*BATTLE STRUCTURE*/
	/*Increment Turn*/
	public void incrementTurn() {turn++;}

	/*Reduce CD of all skills (Testing Only)*/
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

		//cd displays - move to UI class when done writing
		GameObject.Find ("snartscreentext").GetComponent<Text> ().text = "Snartscreen: CD " + System.Convert.ToString (snartscreenToUse);
		GameObject.Find ("sideberntext").GetComponent<Text> ().text = "SideBern: CD " + System.Convert.ToString (sidebernToUse);

		switch(currentState) {
		case(BattleState.PLAYERCHOICE):
			break;
		case(BattleState.PLAYERANIMATE):
			break;
		case(BattleState.ENEMYCHOICE):
			break;
		case(BattleState.ENEMYANIMATE):
			break;
		case(BattleState.DIALOGUE):
			break;
		case(BattleState.WIN):
			break;
		case(BattleState.LOSE):
			break;
		}
	}
}