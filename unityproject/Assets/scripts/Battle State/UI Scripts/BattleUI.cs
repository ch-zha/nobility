using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {

	/*BattleUI detects the UI elements of a battle instance 
	 * and manages outputs.*/

	private Battlemanager STATE;
	private BattleLoad LOAD;

	// Use this for initialization
	void Start () {
		/*Load Partner Scripts*/
		LOAD = this.gameObject.GetComponent<BattleLoad> ();
		STATE = this.gameObject.GetComponent<Battlemanager> ();

		//find GUI elements
		PLAYERHEALTH = GameObject.Find("p1health").GetComponent<Text>();
		ENEMYHEALTH = GameObject.Find("e1health").GetComponent<Text>();
		TURNCOUNT = GameObject.Find("turn").GetComponent<Text>();

		//Determine Player Input UI
		findPlayerUI ();

	}

/*BUTTON HANDLING*/

	public Dropdown teamOne;
	public Button endTurn;

	private void findPlayerUI() {
		teamOne = GameObject.Find ("p1choice").GetComponent<Dropdown> ();
		endTurn = GameObject.Find ("endturn").GetComponent<Button> ();
			}

	private void buttonEnabler() {
		if (STATE.currentState == Battlemanager.BattleState.PLAYERCHOICE) {
			teamOne.enabled = true;
			endTurn.enabled = true;
		} else {
			teamOne.enabled = false;
			endTurn.enabled = false;
		}
	}

	/*Parse player input: move to separate script when layout set*/
	public void optionOne() {
		switch (teamOne.value) {
		case (0):
			break;
		case (1):
			LOAD.TEAM.TEAMMATES[0].selected = Participant.Action.ATTACK;
			break;
		case (2):
			LOAD.TEAM.TEAMMATES[0].selected = Participant.Action.GUARD;
			break;
		}
	}


/*DISPLAYS*/

	private Text PLAYERHEALTH;
	private Text ENEMYHEALTH;
	private Text TURNCOUNT;

	private void displayHealth() {
		//add more characters later
		PLAYERHEALTH.text = "HP:" + System.Convert.ToString(LOAD.TEAM.teamHealth) + "/" + System.Convert.ToString(LOAD.TEAM.teamMaxHealth);
		ENEMYHEALTH.text = "HP:" + System.Convert.ToString(LOAD.ENEMY.teamHealth) + "/" + System.Convert.ToString(LOAD.ENEMY.teamMaxHealth);
	}

	private void displayTurn() {
		TURNCOUNT.text = "Turn:" + System.Convert.ToString (STATE.turn);
	}

	// Update is called once per frame
	void Update () {
		displayHealth ();
		displayTurn ();
		buttonEnabler ();
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 150, 100), STATE.currentState.ToString());
	}
}
