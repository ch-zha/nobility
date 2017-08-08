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

	public BattleState currentState;
	public int turn;

	private BattleLoad LOAD;
	public BattleUI DISPLAY;
	private PlayerOptions OPTIONS;

/*BATTLE STRUCTURE*/
	/*Increment Turn*/
	private void incrementTurn() {turn++;}

/*PLAYER OPTIONS*/
	//?????????????????????????

/*THE GAME, IT DO STUFF*/
	private void chooseEnemyActions() {
		//Debug.Log ("Picking enemy action");

		int action0;
		int action1;
		int action2;

		action0 = EnemyAI.teammateOneDecision (LOAD.ENEMY.TEAMMATES [0]);
		action1 = EnemyAI.teammateTwoDecision (LOAD.ENEMY.TEAMMATES [1]);

		//Debug.Log ("Enemy Action: " + System.Convert.ToString (action));
		switch (action0) {
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

		switch (action1) {
		case (1):
			LOAD.ENEMY.TEAMMATES[1].selected = Participant.Action.ATTACK;
			break;
		case (2):
			LOAD.ENEMY.TEAMMATES[1].selected = Participant.Action.GUARD;
			break;
		case (3):
			LOAD.ENEMY.TEAMMATES [1].selected = Participant.Action.SKILL;
			break;
		}
	}

	private void applyPriorityActions(Participant user) {

		string description;

		switch (user.selected) {
		case (Participant.Action.NONE):
			break;
		case (Participant.Action.ATTACK):
			break;
		case (Participant.Action.GUARD):
			description = System.Convert.ToString (user) + "guarded";
			Debug.Log (description);
			user.equippedUtility.activate();
			DISPLAY.ANIMATIONS.addAnimation (DISPLAY.ANIMATIONS.wait(description));
			break;
		case (Participant.Action.SKILL):
			if (user.skill.hasPriority ()) {
				description = System.Convert.ToString (user) + "used a skill";
				Debug.Log (description);
				user.useSkill ();
				DISPLAY.ANIMATIONS.addAnimation (DISPLAY.ANIMATIONS.wait(description));
			}
			break;
		}
	}

	private void applyCharacterActions(Participant user) {
		string description;
		if (user == null) {
			Debug.Log ("Error: User not found");
			return;
		}
		switch (user.selected) {
		case (Participant.Action.NONE):
			Debug.Log (System.Convert.ToString (user) + "did nothing");
			break;
		case (Participant.Action.ATTACK):
			description = System.Convert.ToString (user) + "attacked";
			Debug.Log (description);
			if (user.TEAM != null) {
				user.equippedAttack.activate ();
			} else {
				Debug.Log ("User.TEAM cannot be accessed");
			}
			DISPLAY.ANIMATIONS.addAnimation (DISPLAY.ANIMATIONS.waitForHealth (new BattleCoroutines.UISnapshot(LOAD, description)));
			break;
		case (Participant.Action.GUARD):
			break;
		case (Participant.Action.SKILL):
			if (!user.skill.hasPriority ()) {
				description = System.Convert.ToString (user) + "used a skill";
				Debug.Log (description);
				user.useSkill ();
				DISPLAY.ANIMATIONS.addAnimation (DISPLAY.ANIMATIONS.waitForHealth (new BattleCoroutines.UISnapshot(LOAD, description)));
			}
			break;
		}
	}

	private void doBattle() {
		foreach (Participant character in sortBySpeed(gatherCharacters())) {
			//Debug.Log (System.Convert.ToString (character));
			if (character != null) {
				applyPriorityActions (character);
			}
		}

		foreach (Participant character in sortBySpeed(gatherCharacters())) {
			//Debug.Log (System.Convert.ToString (character));
			if (character != null) {
				applyCharacterActions (character);
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

	public void endBattle(int scene) {
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
		OPTIONS = this.gameObject.GetComponent<PlayerOptions> ();
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
			//Debug.Log (System.Convert.ToString (DISPLAY.healthUpdated()));
			if (DISPLAY.ANIMATIONS.animationsOver) {
				currentState = BattleState.END;
			}
			break;
		case(BattleState.END):
			LOAD.TEAM.nextTurn ();
			LOAD.ENEMY.nextTurn ();
			DISPLAY.updateUIHealth (true);
			currentState = checkVictory();
			break;
		case(BattleState.DIALOGUE):
			Debug.Log ("DIALOGUE");
			break;
		case(BattleState.WIN):
			Debug.Log ("WIN");
			LOAD.writeTeam ();
			endBattle (3);
			break;
		case(BattleState.LOSE):
			Debug.Log ("LOSE");
			endBattle (2);
			break;
		}
	}
}