using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Skill {

	public override int cost {get; set;}
	private int strength { get; set; }

	public Freeze(int magnitude) {
		cost = magnitude;
		strength = magnitude;
	}

	public override bool hasPriority ()
	{
		return false;
	}

	public override string getName() {
		return "Freeze";
	}

	public override string getDescription() {
		return "Reduces enemies' points";
	}

	public override void activate(TeamStatus self, TeamStatus enemy) {
		enemy.reducePoints (strength);
	}

	public override string getEvent() {
		return "Freeze used";
	}
}
