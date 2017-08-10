using System.Collections;
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

	// Use this for initialization
	void Awake () {
		
		if (GameObject.Find ("GameData") != null) {
			GAMEDATA = GameObject.Find ("GameData").GetComponent<GameData> ();
			TEAM = GAMEDATA.currentTeam;
			TEAM.resetPoints ();
		} else {
			Debug.Log ("No existing game file found.");
		}

		ENEMY = new TeamStatus(new Participant[] {new Participant(new Xenon(Character.BONDSTATE.ENEMY)),
			null, 
			null});

		foreach (Participant teammate in ENEMY.TEAMMATES) {
			if (teammate != null) {
				teammate.setTeam (ENEMY);
			}
		}

		Debug.Log ("ALLIES: " + System.Convert.ToString (TEAM.TEAMMATES[0]) + ", " + System.Convert.ToString (TEAM.TEAMMATES[1]) + ", " + System.Convert.ToString (TEAM.TEAMMATES[2]));
		Debug.Log ("ENEMIES " + System.Convert.ToString (ENEMY.TEAMMATES[0]) + ", " + System.Convert.ToString (ENEMY.TEAMMATES[1]) + ", " + System.Convert.ToString (ENEMY.TEAMMATES[2]));

	}

	public TeamStatus otherTeam(TeamStatus team) {
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
