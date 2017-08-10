using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperAttack : Skill {

	public override int cost {get; set;}
	private float damage { get; set;}

	public SuperAttack(float amount) {
		cost = System.Convert.ToInt16(amount / 20);
		damage = amount;
	}

	public override bool hasPriority ()
	{
		return false;
	}

	public override string getName() {
		return "SuperAttack";
	}

	public override string getDescription() {
		return "A really big attack";
	}

	public override void activate(TeamStatus self, TeamStatus enemy) {
		enemy.reduceHealth(damage);
	}

	public override string getEvent() {
		return "SuperAttack used";
	}
}
