using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BasicGuard : Skill {

	public override Participant user { get; set; }
	public override int cost {get; set;}
	private float defense {get; set;}

	public BasicGuard (float def) {
		cost = System.Convert.ToInt16(def / 25);
		defense = def;
	}

	public override void activate (TeamStatus self, TeamStatus enemy){
		self.addGuard(defense);
	}

	public override bool hasPriority() {
		return true;
	}
	public override string getName () {
		return "Guard";
	}
	public override Skill.SKILLTYPE getType() {
		return Skill.SKILLTYPE.DEFENSE;
	}
	public override string getDescription () {
		return "A basic Guard skill";
	}

	public override string getEvent() {
		return "Guard used";
	}
}
