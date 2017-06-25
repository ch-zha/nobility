using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamStatus {

	public Participant[] TEAMMATES = new Participant[3];

	public float teamMaxHealth { get; set; }
	public float teamHealth { get; set; }

	private float turnGuard;

	public bool allDead = false;

	// Use this for initialization
	public TeamStatus (Participant[] participants) {

		teamMaxHealth = 0;

		if (participants.Length != 3) {
			Debug.Log ("Incorrect participant array length");
		}

		for (int i = 0; i < participants.Length; i++) {
			if (participants[i] != null) {
				TEAMMATES [i] = participants [i];
				teamMaxHealth += TEAMMATES [i].contributeHealth;
			}
		}

		teamHealth = teamMaxHealth;
	}

	public void addGuard(Participant user) {
		turnGuard += user.contributeGuard;
	}

	public void heal(float healamt) {
		teamHealth += healamt;
		if (teamHealth > teamMaxHealth) {
			teamHealth = teamMaxHealth;
		}
	}

	/*TURN RESULTS*/
	public void attack(Participant user) {
		teamHealth -= Mathf.Round (user.currentAttack * (100 - turnGuard)/100);
		if (teamHealth <= 0) {
			teamHealth = 0;
			allDead = true;
		}
	}

	public void useSkill(Participant user) {
		user.skill.activate (this);
		user.cooldown = user.baseCD;
	}
		
	/*PREPARE NEXT TURN*/
	public void updateCDs() {
		foreach (Participant teammate in TEAMMATES) {
			if (teammate != null && teammate.cooldown > 0) {
				teammate.cooldown--;
			}
		}
	}
	public void clearAll() {
		turnGuard = 0;
	}

	public override string ToString ()
	{
		return string.Format ("[TeamStatus: teamMaxHealth={0}]", teamMaxHealth);
	}

}