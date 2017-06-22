using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Participant {
	
	public Character source;

	public float contributeHealth {get; set;}
	public float contributeGuard {get; set;}
	public float currentAttack {get; set;}

	public int cooldown { get; set;}

	public Action selected;
	public enum Action {
		NONE,
		ATTACK,
		GUARD,
		SKILL
	}

	public Participant (Character character) {
		source = character;
		//import if applicable

		contributeHealth = source.BaseHealth;
		contributeGuard = source.BaseGuard;
		currentAttack = source.BaseAttack;
		selected = Action.NONE;
		//cooldown = source.whatever;
	}

	public void clearSelected() {
		selected = Action.NONE;
	}

}
