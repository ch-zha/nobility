using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoad : MonoBehaviour {

	/*BattleLoad detects the appropriate information 
	 * from the game state / character roster and loads
	 * them into the appropriate character/interface slots. */

	//private Battlemanager STATE;
	//private BattleUI DISPLAY;

	public TeamStatus TEAM;
	public TeamStatus ENEMY;

	//junk test code

	// Use this for initialization
	void Awake () {
		/*Load Partner Scripts*/
		//DISPLAY = this.gameObject.GetComponent<BattleUI> ();
		//STATE = this.gameObject.GetComponent<Battlemanager> ();

		//collect character data

		TEAM = new TeamStatus (new Participant[] {new Participant(new Xenon (Character.BONDSTATE.ONE)), new Participant(new Xenon (Character.BONDSTATE.TWO)), new Participant(new Xenon (Character.BONDSTATE.ONE))});
		ENEMY = new TeamStatus (new Participant[] {new Participant (new Xenon(Character.BONDSTATE.ENEMY)), null, null});

		Debug.Log (System.Convert.ToString (TEAM.TEAMMATES[0]) + ", " + System.Convert.ToString (TEAM.TEAMMATES[1]) + ", " + System.Convert.ToString (TEAM.TEAMMATES[2]));
		Debug.Log (System.Convert.ToString (ENEMY.TEAMMATES[0]) + ", " + System.Convert.ToString (ENEMY.TEAMMATES[1]) + ", " + System.Convert.ToString (ENEMY.TEAMMATES[2]));

	}
}
