using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Heal : Skill {

	public override Participant user { get; set; }
	public override int cost {get; set;}
	private float amount { get; set;}

	public Heal(float number) {
		cost = 2 * System.Convert.ToInt16(number / 25);
		amount = number;
	}

	public override bool hasPriority ()
	{
		return false;
	}

	public override string getName() {
		return "Heal";
	}

	public override Skill.SKILLTYPE getType() {
		return Skill.SKILLTYPE.DEFENSE;
	}

	public override string getDescription() {
		return "Heals team by 50";
	}

	public override void activate(TeamStatus self, TeamStatus enemy) {
		self.increaseHealth (amount);
	}

	public override string getEvent() {
		return "Heal used";
	}
}
