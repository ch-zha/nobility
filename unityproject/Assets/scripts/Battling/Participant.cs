using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Participant {
	
	public Character SOURCE;
	public TeamStatus TEAM;

	//public float contributeHealth {get; set;}
	//public float contributeGuard {get; set;}

	public float currentAttack {get; set;}
	public float currentDefense { get; set;}
	public float currentSpeed { get; set;}
	public int cooldown { get; set;}

	public Action selected;
	public enum Action {
		NONE,
		ATTACK,
		GUARD,
		SKILL
	}

	public Skill[] offensive {get; set;}

	public Participant (Character character) {
		SOURCE = character;
		//import if applicable

		currentAttack = SOURCE.BaseAttack;
		currentSpeed = SOURCE.BaseSpeed;
		currentDefense = SOURCE.BaseDefense;
		selected = Action.NONE;
	}

	public void setTeam (TeamStatus team) {
		TEAM = team;
		Debug.Log ("Team set to " + System.Convert.ToString(team));
	}

	public bool skillReady() {
		if (cooldown == 0) {
			return true;
		} else {
			return false;
		}
	}

	public void useSkill() {
		if (! skillReady()) {
			Debug.Log ("Skill still in cooldown");
		} else {
			skill.activate ();
			cooldown = baseCD;
		}
	}

	public void resetSpeed() {
		currentSpeed = SOURCE.BaseSpeed;
	}

	public void changeSpeed(float buff) {
		currentSpeed *= buff;
	}

	public void resetAttack() {
		currentAttack = SOURCE.BaseAttack;
	}

	public void changeAttack(float buff) {
		currentAttack *= buff;
	}

	public void clearSelected() {
		selected = Action.NONE;
	}

	public override string ToString ()
	{
		return string.Format ("Battler: {0}", SOURCE);
	}

}
