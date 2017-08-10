using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Battlemanager : MonoBehaviour {

	/*Battlemanager manages the battle state machine and 
	 * non-team-specific functions.*/

	public enum BattleState {
		PREBATTLE,
		START,
		PLAYERCHOICE,
		ENEMYCHOICE,
		BATTLE,
		ANIMATION,
		END,
		DIALOGUE,
		LOSE,
		WIN
	} 

	public BattleState currentState { get; set; }
	public int turn { get; set; }

	private BattleLoad LOAD { get; set; }
	private BattleUI DISPLAY { get; set; }

/*BATTLE STRUCTURE*/
	/*Increment Turn*/
	private void incrementTurn() {turn++;}

/*PLAYER OPTIONS*/
	//?????????????????????????

/*THE GAME, IT DO STUFF*/

	private void chooseEnemyActions() {
		foreach (Participant enemy in LOAD.ENEMY.TEAMMATES) {
			if (enemy != null) {
				EnemyAI.makeDecision (enemy);
			}
		}
	}

	private void useSkill(Participant character) {
		if (! character.TEAM.exceedsCost(character.selected.cost)) {
			character.TEAM.reducePoints (character.selected.cost);
			character.selected.activate (character.TEAM, LOAD.otherTeam (character.TEAM));
			string description = character.selected.getEvent () + " by " + character.ToString ();
			DISPLAY.ANIMATIONS.addAnimation (DISPLAY.ANIMATIONS.waitForHealth (new BattleCoroutines.UISnapshot (LOAD, description)));
		} else {
			Debug.Log ("Skill cost exceeds available points");
		}
	}

	private void doBattle() {
		foreach (Participant character in sortBySpeed(gatherCharacters())) {
			//Debug.Log (System.Convert.ToString (character));
			if (character != null && character.selected.hasPriority ()) {
				useSkill (character);
			}
		}

		foreach (Participant character in sortBySpeed(gatherCharacters())) {
			//Debug.Log (System.Convert.ToString (character));
			if (character != null && !character.selected.hasPriority()) {
				useSkill (character);
			}
		}
	}

/*GAME FUNCTIONS*/

	private Participant[] gatherCharacters() {
		Participant[] allcharacters = new Participant[6];
		Array.Copy (LOAD.TEAM.TEAMMATES, allcharacters, 3);
		Array.Copy (LOAD.ENEMY.TEAMMATES, 0, allcharacters, 3, 3);

		int junk = 0;
		foreach (Participant character in allcharacters) {
			if (character == null) {
				junk++;
			}
		}

		Participant[] result =  new Participant[6-junk];
		int i = 0;
		foreach (Participant character in allcharacters) {
			if (character != null) {
				result [i] = character;
				i++;
			}
		}

		return result;
	}

	private Participant[] sortBySpeed(Participant[] allcharacters) {
		if (allcharacters == null) {
			Debug.Log ("No array found");
		}

		int length = allcharacters.Length;

		for (int i = 1; i < length; i++) {
			int j = i;
			while ((j > 0) && (allcharacters [j].currentSpeed > allcharacters [j - 1].currentSpeed)) {
				int k = j - 1;
				Participant temp = allcharacters [k];
				allcharacters [k] = allcharacters [j];
				allcharacters [j] = temp;
				j--;
			}
		}
		return allcharacters;
	}

	public void endTurn() {
		if (currentState == BattleState.PLAYERCHOICE) {
			currentState = BattleState.ENEMYCHOICE;
		}
	}

	public void endBattle(string scene) {
		Debug.Log ("Ending Battle");
		UnityEngine.SceneManagement.SceneManager.LoadScene (scene);
	}

	private BattleState checkVictory() {
		if (LOAD.TEAM.allDead == true) {
			return BattleState.LOSE;
		} else if (LOAD.ENEMY.allDead == true) {
			return BattleState.WIN;
		} else {
			return BattleState.START;
		}
	}


/*START*/
	void Start() {
		//initialize turn counters & stuff
		turn = 0;
		currentState = BattleState.PREBATTLE;

		LOAD = this.gameObject.GetComponent<BattleLoad> ();
		DISPLAY = this.gameObject.GetComponent<BattleUI> ();
	}

/*UPDATE*/
	void Update() {
		switch(currentState) {
		case(BattleState.PREBATTLE):
			currentState = BattleState.PLAYERCHOICE;
			break;
		case(BattleState.START):
			Debug.Log ("START");
			incrementTurn ();
			LOAD.TEAM.startTurn ();
			LOAD.ENEMY.startTurn ();
			currentState = BattleState.PLAYERCHOICE;
			break;
		case(BattleState.PLAYERCHOICE):
			break;
		case(BattleState.ENEMYCHOICE):
			chooseEnemyActions ();
			currentState = BattleState.BATTLE;
			break;
		case(BattleState.BATTLE):
			doBattle ();
			DISPLAY.ANIMATIONS.runAnimationQueue ();
			currentState = BattleState.ANIMATION;
			break;
		case(BattleState.ANIMATION):
			if (DISPLAY.ANIMATIONS.animationsOver) {
				currentState = BattleState.END;
			}
			break;
		case(BattleState.END):
			LOAD.TEAM.nextTurn ();
			LOAD.ENEMY.nextTurn ();
			DISPLAY.endTurn ();
			currentState = checkVictory();
			break;
		case(BattleState.DIALOGUE):
			Debug.Log ("DIALOGUE");
			break;
		case(BattleState.WIN):
			Debug.Log ("WIN");
			LOAD.writeTeam ();
			endBattle ("testbattle02");
			break;
		case(BattleState.LOSE):
			Debug.Log ("LOSE");
			endBattle ("GameOver");
			break;
		}
	}
}