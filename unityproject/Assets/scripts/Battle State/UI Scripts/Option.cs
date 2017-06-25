using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour {

	public Participant.Action action;

	public Toggle toggle;
	private BattleLoad LOAD;
	private PlayerOptions controlcenter;

	public bool selected;



	// Use this for initialization
	void Start () {
		LOAD = GameObject.Find ("Scripts").GetComponent<BattleLoad> ();
		controlcenter = GameObject.Find ("Scripts").GetComponent<PlayerOptions> ();
		toggle = this.gameObject.GetComponent<Toggle> ();
		toggle.onValueChanged.AddListener (checkOn);
	}

	private void checkOn(bool toggle) {
		selected = toggle;
		controlcenter.updateOption ();
	}
}
