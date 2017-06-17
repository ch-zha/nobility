using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {

	/*BattleUI detects the UI elements of a battle instance 
	 * and manages outputs.*/

	private Text PLAYERHEALTH;
	private Text TURNCOUNT;

	private Battlemanager STATE;
	private BattleLoad LOAD;

	// Use this for initialization
	void Start () {
		/*Load Partner Scripts*/
		LOAD = this.gameObject.GetComponent<BattleLoad> ();
		STATE = this.gameObject.GetComponent<Battlemanager> ();

		//find GUI elements
		PLAYERHEALTH = GameObject.FindGameObjectWithTag("health").GetComponent<Text>(); //change to tag handling
		TURNCOUNT = GameObject.Find("turn").GetComponent<Text>(); //change to tag handling

	}

	private void displayHealth() {
		//add more characters later
		PLAYERHEALTH.text = "HP:" + System.Convert.ToString(Teamstate.teamstate.player.Health);
	}

	private void displayTurn() {
		TURNCOUNT.text = "Turn:" + System.Convert.ToString (STATE.turn);
	}

	// Update is called once per frame
	void Update () {
		displayHealth ();
		displayTurn ();
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 150, 100), STATE.currentState.ToString());
	}
}
