using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BasicAttack : Skill {

	public override Participant user { get; set; }
	public override int cost {get; set;}

	public BasicAttack (float attack) {
		cost = 2;
	}

	public override void activate (TeamStatus self, TeamStatus enemy){
		enemy.reduceHealth(user.currentAttack);
	}

	public override bool hasPriority() {
		return false;
	}
	public override string getName () {
		return "Attack";
	}
	public override Skill.SKILLTYPE getType() {
		return Skill.SKILLTYPE.OFFENSE;
	}
	public override string getDescription () {
		return "A basic Attack skill";
	}

	public override string getEvent() {
		return "Attack used";
	}

}
