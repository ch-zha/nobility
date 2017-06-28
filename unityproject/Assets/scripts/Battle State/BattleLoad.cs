using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoad : MonoBehaviour {

	/*BattleLoad detects the appropriate information 
	 * from the game state / character roster and loads
	 * them into the appropriate character/interface slots. */

	//private Battlemanager STATE;
	//private BattleUI DISPLAY;
	public GameData GAMEDATA;

	public TeamStatus TEAM;
	public TeamStatus ENEMY;

	//junk test code

	// Use this for initialization
	void Awake () {
		/*Load Partner Scripts*/
		//DISPLAY = this.gameObject.GetComponent<BattleUI> ();
		//STATE = this.gameObject.GetComponent<Battlemanager> ();
		GAMEDATA = GameObject.Find("GameData").GetComponent<GameData>();

		//collect character data

		if (GAMEDATA != null) {
			TEAM = GAMEDATA.currentTeam;
		} else {
			TEAM = new TeamStatus (); 
			TEAM.addParticipants (new Participant[] {
				new Participant (new Xenon (Character.BONDSTATE.ONE), TEAM),
				new Participant (new Helium (Character.BONDSTATE.ONE), TEAM),
				new Participant (new Helium (Character.BONDSTATE.TWO), TEAM)
			});
		}

		ENEMY = new TeamStatus();
		ENEMY.addParticipants(new Participant[] {new Participant (new Xenon(Character.BONDSTATE.ENEMY), ENEMY), null, null});

		Debug.Log ("ALLIES: " + System.Convert.ToString (TEAM.TEAMMATES[0]) + ", " + System.Convert.ToString (TEAM.TEAMMATES[1]) + ", " + System.Convert.ToString (TEAM.TEAMMATES[2]));
		Debug.Log ("ENEMIES " + System.Convert.ToString (ENEMY.TEAMMATES[0]) + ", " + System.Convert.ToString (ENEMY.TEAMMATES[1]) + ", " + System.Convert.ToString (ENEMY.TEAMMATES[2]));

	}

	void Update() {
		//Debug.Log (TEAM.allDead.ToString ());
		//Debug.Log (ENEMY.allDead.ToString ());
	}
}
