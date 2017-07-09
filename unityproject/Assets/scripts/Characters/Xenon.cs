using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Xenon : Character {

	public Xenon (BONDSTATE bondstate) {
		Skill = new AnimeBoy();

		switch (bondstate) {
		case (BONDSTATE.ENEMY):

			STATUS = "ENEMY";

			BaseHealth = 80;
			BaseAttack = 80;
			BaseGuard = 40;
			BaseSpeed = 40;
			Cooldown = 3;
			break;

		case (BONDSTATE.ONE):

			STATUS = "BOND ONE";

			BaseHealth = 80;
			BaseAttack = 20;
			BaseGuard = 20;
			BaseSpeed = 20;

			Cooldown = 3;
			break;

		case (BONDSTATE.TWO):

			STATUS = "BOND TWO";

			BaseHealth = 90;
			BaseAttack = 30;
			BaseGuard = 25;
			BaseSpeed = 30;

			Cooldown = 3;
			break;
		}
	}
		
	[Serializable]
	public class AnimeBoy : Skill {

		public override Participant USER { get; set;}

		public AnimeBoy() {
		}

		public override bool hasPriority ()
		{
			return false;
		}

		public override string getName() {
			return "Anime Boy";
		}

		public override string getDescription() {
			return "Heals team by 50";
		}

		public override void activate() {
			USER.TEAM.heal (50);
		}
	}
		
	private string STATUS;
	public override string ToString ()
	{
		return string.Format ("[Xenon, {0}]", STATUS);
	}
}