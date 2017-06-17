using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill {

	/*Base class for all basic and character-specific skills. Still 
	 * debating whether to use it primarily for inheriting or for 
	 * instancing.*/

	public Skill(int cd) {
		Cooldown = cd;
		//constructor for activate();
	}

	public int Cooldown { get; set; }

	public void activate(Battlemanager battle) {
		//adapt constructor once done testing;
	}

	/*BATTLE FUNCTIONS*/
	//make into separate static class eventually

	/*PLAYER SIDE*/
	public void reduceHealth(Battlemanager battle) {
		//add handling for other characters later
		Teamstate.teamstate.changeHealth(-20);
		if (Teamstate.teamstate.player.Health == 0) {
			//death handling;
		}
	}

	public void increaseHealth(Battlemanager battle) {
		Teamstate.teamstate.changeHealth (20);
	}

	/*ENEMY SIDE*/
	//import enemy stats, functions, etc.

}
