﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoad : MonoBehaviour {

	/*BattleLoad detects the appropriate information 
	 * from the game state / character roster and loads
	 * them into the appropriate character/interface slots. */

	//private Battlemanager STATE;
	//private BattleUI DISPLAY;
	private GameData GAMEDATA;

	public TeamStatus TEAM { get; set; }
	public TeamStatus ENEMY { get; set; }

	//junk test code

	// Use this for initialization
	void Awake () {
		/*Load Partner Scripts*/
		//DISPLAY = this.gameObject.GetComponent<BattleUI> ();
		//STATE = this.gameObject.GetComponent<Battlemanager> ();
		if (GameObject.Find ("GameData") != null) {
			GAMEDATA = GameObject.Find ("GameData").GetComponent<GameData> ();
			TEAM = GAMEDATA.currentTeam;
		} else {
		TEAM = new TeamStatus (new Participant[] {
				new Participant (new Xenon (Character.BONDSTATE.ONE)),
				new Participant (new FistMan (Character.BONDSTATE.ONE)),
				new Participant (new WallE (Character.BONDSTATE.ONE))
			});
		}

		foreach (Participant teammate in TEAM.TEAMMATES) {
			if (teammate != null) {
				teammate.setTeam (TEAM);
			}
		}

		ENEMY = new TeamStatus(TEAM, new Participant[] {
			new Participant (new WallE(Character.BONDSTATE.ENEMY)), 
			new Participant (new WallE(Character.BONDSTATE.ENEMY)), 
				null});

		foreach (Participant teammate in ENEMY.TEAMMATES) {
			if (teammate != null) {
				teammate.setTeam (ENEMY);
			}
		}

		TEAM.otherTeam = ENEMY;

		Debug.Log ("ALLIES: " + System.Convert.ToString (TEAM.TEAMMATES[0]) + ", " + System.Convert.ToString (TEAM.TEAMMATES[1]) + ", " + System.Convert.ToString (TEAM.TEAMMATES[2]));
		Debug.Log ("ENEMIES " + System.Convert.ToString (ENEMY.TEAMMATES[0]) + ", " + System.Convert.ToString (ENEMY.TEAMMATES[1]) + ", " + System.Convert.ToString (ENEMY.TEAMMATES[2]));

	}

	public TeamStatus otherTeam(TeamStatus team) {
		Debug.Log (System.Convert.ToString (team));
		if (team == TEAM) {
			return ENEMY;
		} else if (team == ENEMY) {
			return TEAM;
		} else {
			Debug.Log("Not a valid team.");
			return null;
		}
	}

	public void writeTeam() {
		GAMEDATA.currentTeam.teamHealth = TEAM.teamHealth;
	}

	void Update() {
		//Debug.Log (TEAM.allDead.ToString ());
		//Debug.Log (ENEMY.allDead.ToString ());
	}
}
