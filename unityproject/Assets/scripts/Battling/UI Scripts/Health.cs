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
			SIZE = GameObject.Find("enemyhealthbar").GetComponent<RectTransform> ();
			DISPLAY.playerHealth = this;
		} else if (side == SIDES.TEAM) {
			SIZE = GameObject.Find("playerhealthbar").GetComponent<RectTransform> ();
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
		//Debug.Log (targetPercent.ToString ());
		healthUpdated = false;
		if (barCurrentPercent < targetPercent) {
			//Debug.Log ("Start drawing health bar");
			while (barCurrentPercent < targetPercent) {
				barCurrentPercent += 1F;
				SIZE.localScale = new Vector3 (ORIG_SIZE.x * (barCurrentPercent/100), 1, 1);
				yield return new WaitForSeconds(.1F);
			}
		} else if (barCurrentPercent > targetPercent) {

			Debug.Log (System.Convert.ToString (targetPercent));

			while (barCurrentPercent > targetPercent) {
				//Debug.Log ("Start drawing health bar");
				barCurrentPercent-= 1F;
				//Debug.Log (System.Convert.ToString (barCurrentPercent));
				SIZE.localScale = new Vector3 (ORIG_SIZE.x * (barCurrentPercent/100), 1, 1);
				yield return new WaitForSeconds(.1F);
			}
		} else if (barCurrentPercent == targetPercent) {
			Debug.Log("No adjustments");
			healthUpdated = true;
			yield break;
		}
		healthUpdated = true;
		Debug.Log ("Done drawing");
	}

	public bool healthUpdated;

	public void updateHealth(string healthDisplay, float healthPercent) {
		//Debug.Log ("Updating Health");
		TEXT.text = healthDisplay;
		StartCoroutine (healthChange (healthPercent));
	}

	public void updateHealthNoAnimate (string healthDisplay, float healthPercent) {
		TEXT.text = healthDisplay;
		barCurrentPercent = healthPercent;
		SIZE.localScale = new Vector3 (ORIG_SIZE.x * (barCurrentPercent/100), 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (System.Convert.ToString (healthUpdated));
	}
}
