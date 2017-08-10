using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BasicAttack : Skill {

	public override int cost {get; set;}
	private float damage { get; set;}

	public BasicAttack (float attack) {
		cost = System.Convert.ToInt16(attack / 15);
		damage = attack;
	}

	public override void activate (TeamStatus self, TeamStatus enemy){
		enemy.reduceHealth(damage);
	}

	public override bool hasPriority() {
		return false;
	}
	public override string getName () {
		return "Attack";
	}
	public override string getDescription () {
		return "A basic Attack skill";
	}

	public override string getEvent() {
		return "Attack used";
	}

}
