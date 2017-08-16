using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttack : Skill {

	public override Participant user { get; set; }
	public override int cost {get; set;}

	public SuperAttack(float amount) {
		cost = System.Convert.ToInt16(amount / 20);
	}

	public override bool hasPriority ()
	{
		return false;
	}

	public override string getName() {
		return "SuperAttack";
	}

	public override Skill.SKILLTYPE getType() {
		return Skill.SKILLTYPE.OFFENSE;
	}

	public override string getDescription() {
		return "A really big attack";
	}

	public override void activate(TeamStatus self, TeamStatus enemy) {
		enemy.reduceHealth(2 * user.currentAttack);
	}

	public override string getEvent() {
		return "SuperAttack used";
	}
}
