using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Heal : Skill {

	public override Participant USER { get; set;}

	public Heal() {
	}

	public override bool hasPriority ()
	{
		return false;
	}

	public override string getName() {
		return "Heal";
	}

	public override string getDescription() {
		return "Heals team by 50";
	}

	public override void activate() {
		USER.TEAM.heal (50);
	}
}
