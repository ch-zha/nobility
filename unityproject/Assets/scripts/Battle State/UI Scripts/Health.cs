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

	private BattleLoad LOAD;
	private Text TEXT;
	private RectTransform SIZE;

	private Vector3 ORIG_SIZE;

	// Use this for initialization
	void Start () {
		LOAD = GameObject.Find("Scripts").GetComponent<BattleLoad> ();
		TEXT = this.gameObject.GetComponentInChildren<Text> ();
		SIZE = this.gameObject.GetComponentInChildren<RectTransform> ();
		ORIG_SIZE = SIZE.localScale;
		if (TEXT == null) {
			Debug.Log ("Health text field missing");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (side == SIDES.TEAM) {
			TEXT.text = System.Convert.ToString (LOAD.TEAM.teamHealth + "/" + LOAD.TEAM.teamMaxHealth);
			SIZE.localScale =  new Vector3(ORIG_SIZE.x * LOAD.TEAM.teamHealth / LOAD.TEAM.teamMaxHealth, 1, 1);
		} else if (side == SIDES.ENEMY) {
			TEXT.text = System.Convert.ToString (LOAD.ENEMY.teamHealth + "/" + LOAD.ENEMY.teamMaxHealth);
			SIZE.localScale = new Vector3(ORIG_SIZE.x * LOAD.ENEMY.teamHealth / LOAD.ENEMY.teamMaxHealth, 1, 1);
		}
	}
}
