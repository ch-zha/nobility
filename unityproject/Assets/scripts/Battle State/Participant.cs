using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Participant {
	
	public Character source;

	public float contributeHealth {get; set;}
	public float contributeGuard {get; set;}
	public int baseCD { get; set;}
	public Skill skill {get; set;}

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
		baseCD = source.Cooldown;
		skill = source.Skill;

		currentAttack = source.BaseAttack;
		selected = Action.NONE;
		cooldown = baseCD;
	}

	public bool skillReady() {
		if (cooldown == 0) {
			return true;
		} else {
			return false;
		}
	}

	public void clearSelected() {
		selected = Action.NONE;
	}

	public override string ToString ()
	{
		return string.Format ("Char: {0}", source);
	}

}
