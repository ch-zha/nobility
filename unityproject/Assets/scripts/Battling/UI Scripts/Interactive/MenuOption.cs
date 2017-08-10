using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class MenuOption : MonoBehaviour {

	public enum ASSIGNEDALLY
	{
		One,
		Two,
		Three
	}

	public ASSIGNEDALLY ALLYPOS;

	private Battlemanager STATE { get; set; }
	private BattleLoad LOAD { get; set; }
	private Participant assignedAlly { get; set; }
	public Toggle[] options {get; set; }

	public void changeSelected() {
		for (int i = 0; i < options.Length; i++) {
			if (options [i].isOn) {
				assignedAlly.selected = assignedAlly.useableSkills [i];
			}
		}
		//Debug.Log ("Selected Skill: " + assignedAlly.selected.getName());
		LOAD.TEAM.getPointsInUse();
	}

	public void clearAll() {
		foreach (Toggle option in options) {
			gameObject.GetComponent<ToggleGroup> ().SetAllTogglesOff ();
		}
	}

	public bool exceedsCost(int skill) {
		if (LOAD.TEAM.getPointsInUse () - assignedAlly.selected.cost + assignedAlly.useableSkills [skill].cost > LOAD.TEAM.currentPoints) {
			return true;
		}
		return false;
	}

	void Start () {
		STATE = GameObject.Find ("BattleScripts").GetComponent<Battlemanager> ();
		LOAD = GameObject.Find ("BattleScripts").GetComponent<BattleLoad> ();
		options = this.gameObject.GetComponentsInChildren<Toggle> ();

		switch (ALLYPOS) {
		case (ASSIGNEDALLY.One):
			assignedAlly = LOAD.TEAM.TEAMMATES [0];
			break;
		case (ASSIGNEDALLY.Two):
			assignedAlly = LOAD.TEAM.TEAMMATES [1];
			break;
		case (ASSIGNEDALLY.Three):
			assignedAlly = LOAD.TEAM.TEAMMATES [2];
			break;
		}

		if (assignedAlly == null) {
			foreach (Toggle option in options) {
				option.GetComponent<CanvasGroup> ().alpha = 0;
			}
		} else {
			for (int i = 0; i < options.Length; i++) {
				if (i < assignedAlly.useableSkills.Length) {
					options [i].GetComponentInChildren<Text> ().text = assignedAlly.useableSkills [i].getName () + " (" + assignedAlly.useableSkills [i].cost.ToString() +  ")";
				} else {
					options [i].GetComponent<CanvasGroup> ().alpha = 0;
				}
			}
		}
	}

	void Update() {
		if (STATE.currentState == Battlemanager.BattleState.START) {
			clearAll ();
		} else if (STATE.currentState == Battlemanager.BattleState.PLAYERCHOICE) {
			for (int i = 0; i < assignedAlly.useableSkills.Length; i++) {
				if (exceedsCost(i)) {
					options [i].interactable = false;
				} else {
					options [i].interactable = true;
				}
			}
		}
	}

}
