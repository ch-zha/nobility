using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Participant {
	
	public Character source;

	public float currentHealth {get; set;}
	public float currentAttack {get; set;}
	public bool dead;
	public int cooldown { get; set;}

	public Action selected;
	public enum Action {
		NONE,
		ATTACK
	}

	public Participant (Character character) {
		source = character;
		//import if applicable

		dead = false;
		currentHealth = source.MaxHealth;
		currentAttack = source.BaseAttack;
		selected = Action.NONE;
		//cooldown = source.whatever;
	}

}
