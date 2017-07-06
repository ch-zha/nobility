using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGuard : Skill {

	public override Participant USER { get; set;}

	public BasicGuard (Participant user) {
		USER = user;
	}

	public override void activate (){
		USER.TEAM.addGuard(USER);
	}

	public override bool hasPriority() {
		return true;
	}
	public override string getName () {
		return "Basic Guard";
	}
	public override string getDescription () {
		return "A basic Guard skill";
	}
}
