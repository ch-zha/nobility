using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNone : Skill {

	public override int cost {get; set;}

	public ActionNone () {
		cost = 0;
	}

	public override void activate (TeamStatus self, TeamStatus enemy){
		Debug.Log ("Turn skipped");
	}

	public override bool hasPriority() {
		return false;
	}
	public override string getName () {
		return "Skip";
	}
	public override string getDescription () {
		return "Skip this turn";
	}

	public override string getEvent() {
		return "Turn skipped";
	}
}
