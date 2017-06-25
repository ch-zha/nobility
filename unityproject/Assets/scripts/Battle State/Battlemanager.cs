using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Battlemanager : MonoBehaviour {

	/*Battlemanager manages the battle state machine and 
	 * non-team-specific functions.*/

	public enum BattleState {
		PREBATTLE,
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
	private PlayerOptions OPTIONS;

/*BATTLE STRUCTURE*/
	/*Increment Turn*/
	private void incrementTurn() {turn++;}

/*PLAYER OPTIONS*/
	//?????????????????????????

/*THE GAME, IT DO STUFF*/
	private void chooseEnemyActions() {
		Debug.Log ("Picking enemy action");

		int action = Random.Range (1, 4);
		switch (action) {
		case (1):
			LOAD.ENEMY.TEAMMATES[0].selected = Participant.Action.ATTACK;
			break;
		case (2):
			LOAD.ENEMY.TEAMMATES[0].selected = Participant.Action.GUARD;
			break;
		case (3):
			LOAD.ENEMY.TEAMMATES [0].selected = Participant.Action.SKILL;
			break;
		}
	}

	private void applyPriorityActions(TeamStatus team, Participant user) {
		if (! team.TEAMMATES.Contains(user)) {
			Debug.Log("Character does not belong to that team.");
			return; //add error handling for other users when implemented;
		}

		switch (user.selected) {
		case (Participant.Action.NONE):
			break;
		case (Participant.Action.ATTACK):
			break;
		case (Participant.Action.GUARD):
			Debug.Log (System.Convert.ToString (team) + System.Convert.ToString (user) + "guarded");
			team.addGuard (user);
			break;
		}
	}

	private void applyCharacterActions(TeamStatus team, Participant user) {
		if (! team.TEAMMATES.Contains(user)) {
			Debug.Log("Character does not belong to that team.");
			return; //add error handling for other users when implemented;
		}

		switch (user.selected) {
		case (Participant.Action.NONE):
			//Debug.Log (System.Convert.ToString (team) + System.Convert.ToString (user) + "did nothing");
			break;
		case (Participant.Action.ATTACK):
			//Debug.Log (System.Convert.ToString (team) + System.Convert.ToString (user) + "attacked");
			otherTeam(team).attack (user);
			break;
		case (Participant.Action.GUARD):
			break;
		case (Participant.Action.SKILL):
			team.useSkill (user);
			break;
		}
	}

	private void doBattle() {

		foreach (Participant teammate in LOAD.TEAM.TEAMMATES) {
			if (teammate != null) {
				applyPriorityActions (LOAD.TEAM, teammate);
			}
		}
		foreach (Participant teammate in LOAD.ENEMY.TEAMMATES) {
			if (teammate != null) {
				applyPriorityActions (LOAD.ENEMY, teammate);
			}
		}

		//when speed stat implemented, sort characters by speed into array & implement normal actions through loop;

		foreach (Participant teammate in LOAD.TEAM.TEAMMATES) {
			if (teammate != null) {
				applyCharacterActions (LOAD.TEAM, teammate);
			}
		}
		foreach (Participant teammate in LOAD.ENEMY.TEAMMATES) {
			if (teammate != null) {
				applyCharacterActions (LOAD.ENEMY, teammate);
			}
		}
	}

	private void clearCharacterActions() {
		LOAD.TEAM.clearAll ();
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
		} else {
			Debug.Log ("Starting battle animation");
			animationStarted = true;
			yield return new WaitForSeconds (2);
			doBattle ();
			clearCharacterActions ();
			currentState = BattleState.START;
		}
	}

	public void endTurn() {
		if (currentState == BattleState.PLAYERCHOICE) {
			currentState = BattleState.ENEMYCHOICE;
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
		currentState = BattleState.PREBATTLE;

		LOAD = this.gameObject.GetComponent<BattleLoad> ();
		DISPLAY = this.gameObject.GetComponent<BattleUI> ();
		OPTIONS = this.gameObject.GetComponent<PlayerOptions> ();
	}

/*UPDATE*/
	void Update() {

		checkVictory ();

		switch(currentState) {
		case(BattleState.PREBATTLE):
			currentState = BattleState.PLAYERCHOICE;
			animationStarted = false;
			OPTIONS.cooldownDisable ();
			break;
		case(BattleState.START):
			Debug.Log ("START");
			incrementTurn ();
			animationStarted = false;
			OPTIONS.resetOptions ();
			LOAD.TEAM.updateCDs ();
			OPTIONS.cooldownDisable ();
			OPTIONS.toggleOn (true);
			currentState = BattleState.PLAYERCHOICE;
			break;
		case(BattleState.PLAYERCHOICE):
			break;
		case(BattleState.ENEMYCHOICE):
			OPTIONS.toggleOn (false);
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