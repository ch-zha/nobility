using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public enum SIDES
	{
		TEAM,
		ENEMY
	}
	public SIDES side;

	private BattleUI DISPLAY;
	private Text TEXT;
	private RectTransform SIZE;

	private Vector3 ORIG_SIZE;

	// Use this for initialization
	void Awake () {
		DISPLAY = GameObject.Find("Scripts").GetComponent<BattleUI> ();
		if (DISPLAY == null) {
			Debug.Log ("Can't find DISPLAY");
		}
		TEXT = this.gameObject.GetComponentInChildren<Text> ();
		if (side == SIDES.ENEMY) {
			SIZE = GameObject.Find("enemyhealthbar").GetComponentInChildren<RectTransform> ();
			DISPLAY.playerHealth = this;
		} else if (side == SIDES.TEAM) {
			SIZE = GameObject.Find("playerhealthbar").GetComponentInChildren<RectTransform> ();
			DISPLAY.enemyHealth = this;
		}
		if (TEXT == null) {
			Debug.Log ("Health text field missing");
		}
		ORIG_SIZE = SIZE.localScale;

		barCurrentPercent = 100;
	}

	private float barCurrentPercent;
	IEnumerator healthChange(float targetPercent) {
		healthUpdated = false;
		if (barCurrentPercent < targetPercent) {
			//Debug.Log ("Start drawing health bar");
			while (barCurrentPercent < targetPercent) {
				barCurrentPercent += 1;
				SIZE.localScale = new Vector3 (ORIG_SIZE.x * (barCurrentPercent/100), 1, 1);
				yield return new WaitForFixedUpdate();
			}
		} else if (barCurrentPercent > targetPercent) {

			//Debug.Log (System.Convert.ToString (targetPercent));

			while (barCurrentPercent > targetPercent) {
				//Debug.Log ("Start drawing health bar");
				barCurrentPercent-= 1;
				//Debug.Log (System.Convert.ToString (barCurrentPercent));
				SIZE.localScale = new Vector3 (ORIG_SIZE.x * (barCurrentPercent/100), 1, 1);
				yield return new WaitForFixedUpdate();
			}
		} else if (barCurrentPercent == targetPercent) {
			//Debug.Log("No adjustments");
			healthUpdated = true;
			yield break;
		}
		healthUpdated = true;
		//Debug.Log ("Done drawing");
	}

	public bool healthUpdated;

	public void updateHealth() {
		if (side == SIDES.TEAM) {
			TEXT.text = DISPLAY.playerHealthDisplay;
			//Debug.Log (DISPLAY.playerHealthDisplay);
			StartCoroutine (healthChange (DISPLAY.playerHealthPercent));
		} else if (side == SIDES.ENEMY) {
			TEXT.text = DISPLAY.enemyHealthDisplay;
			//Debug.Log (DISPLAY.enemyHealthDisplay);
			StartCoroutine (healthChange (DISPLAY.enemyHealthPercent));
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (System.Convert.ToString (healthUpdated));
	}
}
