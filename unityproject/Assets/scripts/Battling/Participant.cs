using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Participant {
	
	public Character SOURCE { get; set;}
	public TeamStatus TEAM {get; set;}

	public float currentAttack {get; set;}
	public float currentDefense { get; set;}
	public float currentSpeed { get; set;}

	public Skill[] useableSkills { get; set;}
	public Skill selected {get; set;}

	public Participant (Character character) {
		SOURCE = character;
		//import if applicable

		currentAttack = SOURCE.BaseAttack;
		currentSpeed = SOURCE.BaseSpeed;
		currentDefense = SOURCE.BaseDefense;

		useableSkills = SOURCE.Skills;
		selected = new ActionNone ();
	}

	public void setTeam (TeamStatus team) {
		TEAM = team;
		Debug.Log ("Team set to " + System.Convert.ToString(team));
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
		selected = new ActionNone ();
	}

	public override string ToString ()
	{
		return string.Format ("{0}", SOURCE);
	}

}
