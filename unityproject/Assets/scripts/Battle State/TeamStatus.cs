using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamStatus {

	public Participant teamOne { get; set; }
	public float teamMaxHealth { get; set; }
	public float teamHealth { get; set; }

	private float turnDamage;
	private float turnGuard;

	public bool allDead = false;

	// Use this for initialization
	public TeamStatus (Character one) {
		teamOne = new Participant (one);

		teamMaxHealth = teamOne.contributeHealth; //add other teammates when implemented;
		teamHealth = teamMaxHealth;
	}

	//damage to be inflicted on THIS team this turn
	public void addDamage (Participant user) {
		turnDamage += user.currentAttack;
	}

	public void addGuard(Participant user) {
		turnGuard += user.contributeGuard;
	}

	/*TURN RESULTS*/
	public void attack(Participant user) {
		teamHealth -= Mathf.Round (user.currentAttack * (100 - turnGuard)/100);
		if (teamHealth <= 0) {
			teamHealth = 0;
			allDead = true;
		}
	}

	/*RESET NEXT TURN*/
	public void clearAll() {
		turnGuard = 0;
		turnDamage = 0;
		teamOne.clearSelected ();
	}

}