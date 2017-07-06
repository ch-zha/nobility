using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : Skill {

	public override Participant USER { get; set;}

	public BasicAttack (Participant user) {
		USER = user;
	}

	public override void activate (){
		USER.TEAM.otherTeam.attack(USER);
	}

	public override bool hasPriority() {
		return false;
	}
	public override string getName () {
		return "Basic Attack";
	}
	public override string getDescription () {
		return "A basic Attack skill";
	}

}
