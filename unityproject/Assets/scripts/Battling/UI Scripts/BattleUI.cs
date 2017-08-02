using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {

	/*BattleUI detects the UI elements of a battle instance 
	 * and manages outputs.*/

	private Battlemanager STATE;
	private BattleLoad LOAD;
	public BattleCoroutines ANIMATIONS;
	public CameraParallax CAMERA;
	public Text ACTIONDESC;

	private Health[] HEALTHBARS;

	// Use this for initialization
	void Start () {
		/*Load Partner Scripts*/
		LOAD = this.gameObject.GetComponent<BattleLoad> ();
		STATE = this.gameObject.GetComponent<Battlemanager> ();
		CAMERA = GameObject.Find("BattleCam").GetComponent<CameraParallax> ();
		ANIMATIONS = new BattleCoroutines (this);

		playerHealth = GameObject.Find ("PlayerHealth").GetComponent<Health> ();
		enemyHealth = GameObject.Find ("EnemyHealth").GetComponent<Health> ();

		ACTIONDESC = GameObject.Find ("Action").GetComponent<Text> ();

		updateUIHealth (false);
	}
		
	/*HEALTH*/

	public Health playerHealth;
	public Health enemyHealth;

	public void clearActionDescription() {
		ACTIONDESC.text = "";
	}

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
		Debug.Log (System.Convert.ToString(enemyHealthPercent));
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 150, 100), STATE.currentState.ToString());
	}

	public bool healthUpdated() {
		return enemyHealth.healthUpdated & playerHealth.healthUpdated;
	}

	void Update() {
		//Debug.Log ("Enemy health updated:" + System.Convert.ToString(enemyHealth.healthUpdated));
		//Debug.Log ("Player health updated:" + System.Convert.ToString(playerHealth.healthUpdated));
	}
}
