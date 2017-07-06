using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallE : Character {

	public WallE (BONDSTATE bondstate) {
		Skill = new SuperWall();

		switch (bondstate) {
		case (BONDSTATE.ENEMY):
			Debug.Log("Error: WallE cannot assume that status.");
			break;

		case (BONDSTATE.ONE):

			STATUS = "BOND ONE";

			BaseHealth = 150;
			BaseAttack = 10;
			BaseGuard = 40;
			BaseSpeed = 11;

			Cooldown = 4;
			break;

		case (BONDSTATE.TWO):
			break;
		}
	}

	public class SuperWall : Skill {

		public override Participant USER { get; set;}

		public SuperWall() {
		}

		public override bool hasPriority ()
		{
			return true;
		}

		public override string getName() {
			return "SuperWall";
		}

		public override string getDescription() {
			return "Blocks all incoming attack damage for a turn";
		}

		public override void activate() {
			USER.TEAM.addGuard(100);
		}
	}

	private string STATUS;
	public override string ToString ()
	{
		return string.Format ("[WallE, {0}]", STATUS);
	}

}
