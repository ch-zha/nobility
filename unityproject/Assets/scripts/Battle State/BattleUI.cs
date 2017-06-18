using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {

	/*BattleUI detects the UI elements of a battle instance 
	 * and manages outputs.*/

	private Battlemanager STATE;
	//private BattleLoad LOAD;
	private Playerstatus TEAM;
	private Enemystatus ENEMY;

	// Use this for initialization
	void Start () {
		/*Load Partner Scripts*/
		//LOAD = this.gameObject.GetComponent<BattleLoad> ();
		STATE = this.gameObject.GetComponent<Battlemanager> ();
		TEAM = this.gameObject.GetComponent<Playerstatus> ();
		ENEMY = this.gameObject.GetComponent<Enemystatus> ();

		//find GUI elements
		PLAYERHEALTH = GameObject.Find("p1health").GetComponent<Text>();
		ENEMYHEALTH = GameObject.Find("e1health").GetComponent<Text>();
		TURNCOUNT = GameObject.Find("turn").GetComponent<Text>();

		//Determine Player Input UI
		findPlayerUI ();

	}

/*BUTTON HANDLING*/

	public Dropdown teamOne;

	private void findPlayerUI() {
		teamOne = GameObject.Find ("p1choice").GetComponent<Dropdown> ();
			}

	private void buttonEnabler() {
		if (STATE.currentState == Battlemanager.BattleState.PLAYERCHOICE) {
			teamOne.enabled = true;
		} else {
			teamOne.enabled = false;
		}
	}


/*DISPLAYS*/

	private Text PLAYERHEALTH;
	private Text ENEMYHEALTH;
	private Text TURNCOUNT;

	private void displayHealth() {
		//add more characters later
		PLAYERHEALTH.text = "HP:" + System.Convert.ToString(TEAM.teamOne.currentHealth);
		ENEMYHEALTH.text = "HP:" + System.Convert.ToString(ENEMY.enemyOne.currentHealth);
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
