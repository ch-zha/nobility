using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TeamStatus {

	public TeamStatus otherTeam;
	public Participant[] TEAMMATES = new Participant[3];

	public float teamMaxHealth { get; set; }
	public float teamHealth { get; set; }

	private float turnGuard;
	private List<StatusEffect> statusEffects;

	public bool allDead = false;

	/*INIT*/
	public TeamStatus(TeamStatus otherteam, Participant[] participants) {
		new TeamStatus (participants);
		otherTeam = otherteam;
	}

	public TeamStatus(Participant[] participants) {

		teamMaxHealth = 150;
		teamHealth = teamMaxHealth;

		if (participants.Length != 3) {
			Debug.Log ("Incorrect participant array length");
		}

		for (int i = 0; i < participants.Length; i++) {
			if (participants[i] != null) {
				TEAMMATES [i] = participants [i];
			}
		}

		teamHealth = teamMaxHealth;
		statusEffects = new List<StatusEffect> ();
	}

	/*EDIT TEAM*/

	public void addGuard (Participant user) {
		addGuard (user.currentDefense);
	}

	public void addGuard(float guardAmount) {
		turnGuard += guardAmount;
		if (turnGuard > 100) {
			turnGuard = 100;
		} else if (turnGuard < 0) {
			turnGuard = 0;
		}
		//Debug.Log ("Guard Amt. " + System.Convert.ToString (turnGuard));
	}

	public void heal(float healamt) {
		teamHealth += healamt;
		if (teamHealth > teamMaxHealth) {
			teamHealth = teamMaxHealth;
		}
	}

	/*TURN RESULTS*/
	public void attack (float damage) {
		teamHealth -= Mathf.Round (damage * (100 - turnGuard) / 100);
		if (teamHealth <= 0) {
			teamHealth = 0;
			allDead = true;
		}
	}

	public void attack(Participant user) {
		attack (user.currentAttack);
	}

	/*CREATE STATUS*/
	public void createNewStatus(StatusEffect.EFFECT effect, int turns, float magnitude) {
		StatusEffect status = new StatusEffect (effect, turns, magnitude, this);
		statusEffects.Add (status);
		Debug.Log (System.Convert.ToString (status) + "added");
	}

	/*LASTING EFFECTS*/
	private void changeAllSpeed(float buff) {
		foreach (Participant teammate in TEAMMATES) {
			teammate.changeSpeed (buff);
			Debug.Log (System.Convert.ToString (teammate) + " Speed: " + System.Convert.ToString (teammate.currentSpeed));
		}
	}

	private void resetAllSpeed() {
		foreach (Participant teammate in TEAMMATES) {
			teammate.resetSpeed();
			Debug.Log (System.Convert.ToString (teammate) + " Speed: " + System.Convert.ToString (teammate.currentSpeed));
		}
	}

	/*PREPARE NEXT TURN*/
	public void updateCDs() {
		foreach (Participant teammate in TEAMMATES) {
			if (teammate != null && teammate.cooldown > 0) {
				teammate.cooldown--;
			}
		}
	}

	public void nextTurn() {
		turnGuard = 0;
		//Debug.Log ("Turn Guard reset");

		List<StatusEffect> toDelete = new List<StatusEffect>();
		foreach (StatusEffect status in statusEffects) {
			status.nextTurn ();
			if (status.removeMe) {
				toDelete.Add (status);
			}
		}

		foreach (StatusEffect status in toDelete) {
			statusEffects.Remove (status);
			Debug.Log (System.Convert.ToString (status) + " deleted");
		}
	}

	public override string ToString ()
	{
		return string.Format ("[TeamStatus: teamMaxHealth={0}]", teamMaxHealth);
	}

	public class StatusEffect {

		public enum EFFECT {
			SPEED,
			ATTACK
		}

		private EFFECT effect;
		private float magnitude;
		private int turnsLeft;
		private TeamStatus team;
		public bool removeMe { get; set; }

		public StatusEffect(EFFECT effectApplied, int turns, float howBig, TeamStatus teamFor) {
			effect = effectApplied;
			turnsLeft = turns;
			team = teamFor;
			magnitude = howBig;
			removeMe = false;

			applyEffect();
		}

		public void nextTurn() {
			if (turnsLeft > 0) {
				turnsLeft--;
			} else if (turnsLeft == 0) {
				endEffect ();
				removeMe = true;
			} else {
				Debug.Log ("Status should be removed");
			}
		}

		private void applyEffect() {
			switch (effect) {
			case (EFFECT.ATTACK):
				break;
			case (EFFECT.SPEED):
				team.changeAllSpeed (magnitude);
				break;
			}
		}

		private void endEffect() {
			switch (effect) {
			case (EFFECT.ATTACK):
				break;
			case (EFFECT.SPEED):
				team.resetAllSpeed();
				break;
			}
		}

		public override string ToString ()
		{
			return string.Format ("[StatusEffect: {0}, Turns Left: {1}, Magnitude: {2}]", effect, turnsLeft, magnitude);
		}
	}

}