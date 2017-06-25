using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour {

	/*BattleUI detects the UI elements of a battle instance 
	 * and manages outputs.*/

	private Battlemanager STATE;
	private BattleLoad LOAD;

	private Health[] HEALTHBARS;

	// Use this for initialization
	void Start () {
		/*Load Partner Scripts*/
		LOAD = this.gameObject.GetComponent<BattleLoad> ();
		STATE = this.gameObject.GetComponent<Battlemanager> ();

		//playerHealth = GameObject.Find ("PlayerHealth").GetComponent<Health> ();
		//enemyHealth = GameObject.Find ("EnemyHealth").GetComponent<Health> ();

		updateUIHealth ();
	}
		
	/*HEALTH*/

	public Health playerHealth;
	public Health enemyHealth;

	public float playerHealthPercent { get; set; }
	public float enemyHealthPercent { get; set; }

	public string playerHealthDisplay { get; set; }
	public string enemyHealthDisplay { get; set; }

	public void updateUIHealth() {
		playerHealthPercent = Mathf.Round (LOAD.TEAM.teamHealth / LOAD.TEAM.teamMaxHealth * 100);
		enemyHealthPercent = Mathf.Round (LOAD.ENEMY.teamHealth / LOAD.ENEMY.teamMaxHealth * 100);

		playerHealthDisplay = System.Convert.ToString (LOAD.TEAM.teamHealth + "/" + LOAD.TEAM.teamMaxHealth);
		enemyHealthDisplay = System.Convert.ToString (LOAD.ENEMY.teamHealth + "/" + LOAD.ENEMY.teamMaxHealth);

		playerHealth.updateHealth ();
		enemyHealth.updateHealth ();

		//Debug.Log (playerHealthDisplay);
		//Debug.Log (enemyHealthDisplay);
		//Debug.Log (System.Convert.ToString(playerHealthPercent));
		//Debug.Log (System.Convert.ToString(enemyHealthPercent));
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 10, 150, 100), STATE.currentState.ToString());
	}

	public bool healthUpdated() {
		return enemyHealth.healthUpdated & playerHealth.healthUpdated;
	}

	void Update() {
	}
}
