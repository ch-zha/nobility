using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TeamStatus {

	public Participant[] TEAMMATES = new Participant[3];

	public float teamMaxHealth { get; set; }
	public float teamHealth { get; set; }

	public int currentPoints { get; set; }
	public int pointsInUse { get; set; }
	private float turnGuard {get; set; }
	private Skill.SKILLTYPE comboType { get; set; }
	private List<StatusEffect> statusEffects;

	public bool allDead {get; set;}

	private int defaultPoints = 6;

	/*INIT*/

	public TeamStatus(Participant[] participants) {

		teamMaxHealth = 150;
		teamHealth = teamMaxHealth;
		turnGuard = 0;
		currentPoints = defaultPoints;
		pointsInUse = 0;
		statusEffects = new List<StatusEffect> ();
		allDead = false;

		if (participants.Length != 3) {
			Debug.Log ("Incorrect participant array length");
		}

		for (int i = 0; i < participants.Length; i++) {
			if (participants[i] != null) {
				TEAMMATES [i] = participants [i];
			}
		}
	}

	/*TURN ACTIONS*/

	public int checkSkillType(Skill.SKILLTYPE type) {
		int i = 0;
		foreach (Participant teammate in TEAMMATES) {
			if (teammate.selected.getType () == type) {
				i++;
			}
		}
		return i;
	}

	private bool isAllTeammates(int count) {
		int i = 0;
		foreach (Participant teammate in TEAMMATES) {
			if (teammate != null) {
				i++;
			}
		}
		if (count == i) {
			return true;
		} else {
			return false;
		}
	}

	public Skill.SKILLTYPE checkCombo() {
		if (isAllTeammates (checkSkillType (Skill.SKILLTYPE.DEFENSE))) {
			return Skill.SKILLTYPE.DEFENSE;
		} else if (isAllTeammates (checkSkillType (Skill.SKILLTYPE.OFFENSE))) {
			return Skill.SKILLTYPE.OFFENSE;
		} else {
			return Skill.SKILLTYPE.NONE;
		}
	}

	public void applyCombo() {
		Skill.SKILLTYPE type = checkCombo ();
		switch (type) {
		case (Skill.SKILLTYPE.OFFENSE):
			break;
		case (Skill.SKILLTYPE.DEFENSE):
			break;
		case (Skill.SKILLTYPE.NONE):
			break;
		}
	}

	public void addPoints(int points) {
		currentPoints += points;
	}

	public void resetPoints () {
		currentPoints = defaultPoints;
	}

	public void reducePoints (int cost) {
		currentPoints -= cost;
		if (currentPoints < 0) {
			currentPoints = 0;
		}
	}

	public bool exceedsCost (int cost) {
		if (cost > currentPoints) {
			return true;
		} else {
			return false;
		}
	}

	public int getPointsInUse() {
		int total = 0;
		foreach (Participant teammate in TEAMMATES) {
			if (teammate != null) {
				total += teammate.selected.cost;
			}
		}
		pointsInUse = total;
		return pointsInUse;
	}

	public void startTurn() {
		addPoints (defaultPoints);
		getPointsInUse ();
	}

	public void nextTurn() {
		turnGuard = 0;
		foreach (Participant teammate in TEAMMATES) {
			if (teammate != null) {
				teammate.clearSelected ();
			}
		}
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

	/*EDIT TEAM*/

	public void addGuard(float guardAmount) {
		turnGuard += guardAmount;
		if (turnGuard > 100) {
			turnGuard = 100;
		} else if (turnGuard < 0) {
			turnGuard = 0;
		}
		//Debug.Log ("Guard Amt. " + System.Convert.ToString (turnGuard));
	}

	public void increaseHealth (float healamt) {
		teamHealth += healamt;
		if (teamHealth > teamMaxHealth) {
			teamHealth = teamMaxHealth;
		}
	}
		
	public void reduceHealth (float damage) {
		teamHealth -= Mathf.Round (damage * (100 - turnGuard) / 100);
		if (teamHealth <= 0) {
			teamHealth = 0;
			allDead = true;
		}
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