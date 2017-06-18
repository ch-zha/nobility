using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battlemanager : MonoBehaviour {

	/*Battlemanager manages the battle state machine and 
	 * non-team-specific functions.*/

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

	//private BattleLoad LOAD;
	private BattleUI DISPLAY;
	private Playerstatus TEAM;
	private Enemystatus ENEMY;

/*BATTLE STRUCTURE*/
	/*Increment Turn*/
	private void incrementTurn() {turn++;}

/*PLAYER OPTIONS*/

	public void optionOne() {
		switch (DISPLAY.teamOne.value) {
		case (0):
			break;
		case (1):
			TEAM.teamOne.selected = Participant.Action.ATTACK;
			break;
		}
	}

/*THE GAME, IT DO STUFF*/
	private void doPlayerStuff() {
		switch (TEAM.teamOne.selected) {
		case (Participant.Action.ATTACK):
			Basic.attack (TEAM.teamOne, ENEMY.enemyOne);
			break;
		}
	}

	private void clearCharacterActions() { 
		TEAM.teamOne.selected = Participant.Action.NONE;
		DISPLAY.teamOne.value = 0;
	}

	private void enemyActions() {
		Basic.attack (ENEMY.enemyOne, TEAM.teamOne);
	}

/*GAME FUNCTIONS*/
	public void endTurn() {
		if (currentState == BattleState.PLAYERCHOICE) {
			currentState = BattleState.PLAYERANIMATE;
		} else if (currentState == BattleState.ENEMYCHOICE) {
			currentState = BattleState.ENEMYANIMATE;
		}
	}

	public void endBattle() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}

	public void checkVictory() {
		if (TEAM.allDead == true) {
			currentState = BattleState.LOSE;
		} else if (ENEMY.allDead == true) {
			currentState = BattleState.WIN;
		}
	}


/*START*/
	void Start() {
		//initialize turn counters & stuff
		turn = 0;
		currentState = BattleState.START;

		//LOAD = this.gameObject.GetComponent<BattleLoad> ();
		DISPLAY = this.gameObject.GetComponent<BattleUI> ();
		TEAM = this.gameObject.GetComponent<Playerstatus> ();
		ENEMY = this.gameObject.GetComponent<Enemystatus> ();
	}

/*UPDATE*/
	void Update() {

		checkVictory ();

		switch(currentState) {
		case(BattleState.START):
			currentState = BattleState.PLAYERCHOICE;
			break;
		case(BattleState.PLAYERCHOICE):
			break;
		case(BattleState.PLAYERANIMATE):
			doPlayerStuff ();
			clearCharacterActions ();
			currentState = BattleState.ENEMYCHOICE;
			break;
		case(BattleState.ENEMYCHOICE):
			break;
		case(BattleState.ENEMYANIMATE):
			enemyActions ();
			incrementTurn();
			currentState = BattleState.PLAYERCHOICE;
			break;
		case(BattleState.DIALOGUE):
			break;
		case(BattleState.WIN):
			endBattle ();
			break;
		case(BattleState.LOSE):
			endBattle ();
			break;
		}
	}
}