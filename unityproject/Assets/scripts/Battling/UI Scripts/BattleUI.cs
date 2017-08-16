using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleUI : MonoBehaviour {

	/*BattleUI detects the UI elements of a battle instance 
	 * and manages outputs.*/

	private BattleLoad LOAD { get; set;}
	private Battlemanager STATE {get; set;}
	public BattleCoroutines ANIMATIONS {get; set;}
	public CameraParallax CAMERA { get; set;}

	public Text POINTS;
	public Text ENEMYPOINTS;
	public Text CURRENTMOVE;
	public Text COMBODISPLAY;

	private Health[] HEALTHBARS;
	public Health playerHealth { get; set;}
	public Health enemyHealth {get; set;}

	// Use this for initialization
	void Start () {
		/*Load Partner Scripts*/
		LOAD = this.gameObject.GetComponent<BattleLoad> ();
		STATE = this.gameObject.GetComponent<Battlemanager> ();
		CAMERA = GameObject.Find("BattleCam").GetComponent<CameraParallax> ();
		ANIMATIONS = new BattleCoroutines (this);

		playerHealth = GameObject.Find ("PlayerHealth").GetComponent<Health> ();
		enemyHealth = GameObject.Find ("EnemyHealth").GetComponent<Health> ();

		//ACTIONDESC = GameObject.Find ("Action").GetComponent<Text> ();

		updateUIHealth (false);
	}

	/*END TURN*/

	public void endTurn() {
		updateUIHealth (false);
		LOAD.TEAM.getPointsInUse ();
		LOAD.ENEMY.getPointsInUse ();
	}
		
	/*HEALTH*/

	public void updateUIHealth(bool animate) {
		//Debug.Log ("Calibrating UI Display");
		updateUIHealth (LOAD.TEAM.teamHealth, LOAD.TEAM.teamMaxHealth, LOAD.ENEMY.teamHealth, LOAD.ENEMY.teamMaxHealth, animate);
	}

	public void updateUIHealth(BattleCoroutines.UISnapshot snapshot) {
		//Debug.Log ("Uploading snapshot");
		updateUIHealth (snapshot.playerHealth, snapshot.playerMaxHealth, snapshot.enemyHealth, snapshot.enemyMaxHealth, true);
	}

	public void updateUIHealth(float playerCurrent, float playerMax, float enemyCurrent, float enemyMax, bool animate) {
		float playerHealthPercent = Mathf.Round (playerCurrent / playerMax * 100);
		float enemyHealthPercent = Mathf.Round (enemyCurrent / enemyMax * 100);

		string playerHealthDisplay = System.Convert.ToString (playerCurrent + "/" + playerMax);
		string enemyHealthDisplay = System.Convert.ToString (enemyCurrent + "/" + enemyMax);

		if (animate) {
			playerHealth.updateHealth (playerHealthDisplay, playerHealthPercent);
			enemyHealth.updateHealth (enemyHealthDisplay, enemyHealthPercent);
		} else {
			playerHealth.updateHealthNoAnimate (playerHealthDisplay, playerHealthPercent);
			enemyHealth.updateHealthNoAnimate (enemyHealthDisplay, enemyHealthPercent);
		}

		//Debug.Log (playerHealthDisplay);
		//Debug.Log (enemyHealthDisplay);
		//Debug.Log (System.Convert.ToString(playerHealthPercent));
		//Debug.Log (System.Convert.ToString(enemyHealthPercent));
	}

	public bool healthUpdated() {
		return enemyHealth.healthUpdated & playerHealth.healthUpdated;
	}

	public void updatePoints(BattleCoroutines.UISnapshot snapshot) {
		POINTS.text = "Current Points: " + snapshot.playerPoints.ToString ();
		ENEMYPOINTS.text = "Current Points: " + snapshot.enemyPoints.ToString ();
	}

	void Update() {
		//Debug.Log ("Enemy health updated:" + System.Convert.ToString(enemyHealth.healthUpdated));
		//Debug.Log ("Player health updated:" + System.Convert.ToString(playerHealth.healthUpdated));

		if (STATE.currentState == Battlemanager.BattleState.PLAYERCHOICE) {
			ENEMYPOINTS.text = LOAD.ENEMY.currentPoints.ToString ();
			POINTS.text = System.Convert.ToString ("Current: " + LOAD.TEAM.currentPoints + "; In Use: " + LOAD.TEAM.getPointsInUse ());
		}

		if (STATE.currentState != Battlemanager.BattleState.ANIMATION) {
			CURRENTMOVE.text = " ";
		}

		COMBODISPLAY.text = string.Format ("Offense: {0}{1}Defense: {2}{3}Combo: {4}", 
			LOAD.TEAM.checkSkillType (Skill.SKILLTYPE.OFFENSE).ToString(), 
			Environment.NewLine, LOAD.TEAM.checkSkillType (Skill.SKILLTYPE.DEFENSE).ToString(), 
			Environment.NewLine, LOAD.TEAM.checkCombo ().ToString());
	}
}
