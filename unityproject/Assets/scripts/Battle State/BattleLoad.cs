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

	// Use this for initialization
	void Start () {
		/*Load Partner Scripts*/
		//DISPLAY = this.gameObject.GetComponent<BattleUI> ();
		//STATE = this.gameObject.GetComponent<Battlemanager> ();

		//collect character data

		TEAM = new TeamStatus (new Windblade (1));
		ENEMY = new TeamStatus (new Lonk());

	}
}
