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
		ENEMYCHOICE,
		ATTACKANIMATE,
		DIALOGUE,
		LOSE,
		WIN
	} 

	public BattleState currentState;
	public int turn;

	private BattleLoad LOAD;
	private BattleUI DISPLAY;

/*BATTLE STRUCTURE*/
	/*Increment Turn*/
	private void incrementTurn() {turn++;}

/*PLAYER OPTIONS*/
	//?????????????????????????

/*THE GAME, IT DO STUFF*/
	private void chooseEnemyActions() {
		Debug.Log ("Picking enemy action");

		int action = Random.Range (1, 3);
		switch (action) {
		case (1):
			LOAD.ENEMY.teamOne.selected = Participant.Action.ATTACK;
			break;
		case (2):
			LOAD.ENEMY.teamOne.selected = Participant.Action.GUARD;
			break;
		}
	}

	private void applyPriorityActions(TeamStatus team, Participant user) {
		if (team.teamOne != user) {
			Debug.Log("Character does not belong to that team.");
			return; //add error handling for other users when implemented;
		}

		switch (user.selected) {
		case (Participant.Action.NONE):
			break;
		case (Participant.Action.ATTACK):
			break;
		case (Participant.Action.GUARD):
			team.addGuard (user);
			break;
		}
	}

	private void applyCharacterActions(TeamStatus team, Participant user) {
		if (team.teamOne != user) {
			Debug.Log("Character does not belong to that team.");
			return; //add error handling for other users when implemented;
		}

		switch (user.selected) {
		case (Participant.Action.NONE):
			break;
		case (Participant.Action.ATTACK):
			otherTeam(team).attack (user);
			break;
		case (Participant.Action.GUARD):
			break;
		}
	}

	private void doBattle() {
		Debug.Log ("Doing battle.");

		applyPriorityActions (LOAD.TEAM, LOAD.TEAM.teamOne);
		applyPriorityActions (LOAD.ENEMY, LOAD.ENEMY.teamOne);

		//when speed stat implemented, sort characters by speed into array & implement normal actions through loop;

		applyCharacterActions (LOAD.TEAM, LOAD.TEAM.teamOne);
		applyCharacterActions (LOAD.ENEMY, LOAD.ENEMY.teamOne);
	}

	private void clearCharacterActions() {
		Debug.Log ("Clearing Actions");

		LOAD.TEAM.teamOne.selected = Participant.Action.NONE; //change to a function inside TeamStatus;
		DISPLAY.teamOne.value = 0; //change to a function inside BattleUI
		LOAD.TEAM.clearAll();
		LOAD.ENEMY.clearAll ();
	}

/*GAME FUNCTIONS*/
	private TeamStatus otherTeam(TeamStatus team) {
		if (team == LOAD.TEAM) {
			return LOAD.ENEMY;
		} else if (team == LOAD.ENEMY) {
			return LOAD.TEAM;
		} else {
			Debug.Log("Not a valid team.");
			return null;
		}
	}

	private bool animationStarted = false;
	IEnumerator BattleAnimation() {
		if (animationStarted == true) {
			yield break;
		}
		Debug.Log ("Starting battle animation");
		animationStarted = true;
		yield return new WaitForSeconds(2);
		doBattle ();
		clearCharacterActions ();
		currentState = BattleState.START;
	}

	public void endTurn() {
		if (currentState == BattleState.PLAYERCHOICE) {
			currentState = BattleState.ENEMYCHOICE;
		} else if (currentState == BattleState.ENEMYCHOICE) {
			currentState = BattleState.ATTACKANIMATE;
		}
	}

	public void endBattle() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}

	private void checkVictory() {
		if (LOAD.TEAM.allDead == true) {
			currentState = BattleState.LOSE;
		} else if (LOAD.ENEMY.allDead == true) {
			currentState = BattleState.WIN;
		}
	}


/*START*/
	void Start() {
		//initialize turn counters & stuff
		turn = 0;
		currentState = BattleState.START;

		LOAD = this.gameObject.GetComponent<BattleLoad> ();
		DISPLAY = this.gameObject.GetComponent<BattleUI> ();
	}

/*UPDATE*/
	void Update() {

		checkVictory ();

		switch(currentState) {
		case(BattleState.START):
			Debug.Log ("START");
			incrementTurn ();
			animationStarted = false;
			currentState = BattleState.PLAYERCHOICE;
			break;
		case(BattleState.PLAYERCHOICE):
			break;
		case(BattleState.ENEMYCHOICE):
			Debug.Log("ENEMY CHOICE");
			chooseEnemyActions ();
			currentState = BattleState.ATTACKANIMATE;
			break;
		case(BattleState.ATTACKANIMATE):
			StartCoroutine (BattleAnimation ());
			break;
		case(BattleState.DIALOGUE):
			Debug.Log ("DIALOGUE");
			break;
		case(BattleState.WIN):
			Debug.Log("WIN");
			endBattle ();
			break;
		case(BattleState.LOSE):
			Debug.Log ("LOSE");
			endBattle ();
			break;
		}
	}
}