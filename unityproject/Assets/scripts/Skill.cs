using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill {

	public Skill(int cd) {
		Cooldown = cd;
		//constructor for activate();
	}

	public int Cooldown { get; set; }

	public void activate(Battlemanager battle) {
		//fit to constructor once done testing;
	}

	/*BATTLE FUNCTIONS*/
	//make into separate static class eventually

	/*PLAYER SIDE*/
	public void reduceHealth(Battlemanager battle) {
		//add handling for other characters later
		Teamstate.teamstate.changeHealth(-20);
		battle.incrementTurn (); //delete when activate() is complete
		if (Teamstate.teamstate.player.Health == 0) {
			battle.endBattle ();
		}
	}

	public void increaseHealth(Battlemanager battle) {
		Teamstate.teamstate.changeHealth (20);
		battle.incrementTurn (); //delete when activate() is complete
	}

	/*ENEMY SIDE*/
	//import enemy stats, functions, etc.

}
