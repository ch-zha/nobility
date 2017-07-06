using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistMan : Character {

	public FistMan (BONDSTATE bondstate) {
		Skill = new PunchyPunch();

		switch (bondstate) {
		case (BONDSTATE.ENEMY):
			Debug.Log("Error: FistMan cannot assume that status.");
			break;

		case (BONDSTATE.ONE):

			STATUS = "BOND ONE";

			BaseHealth = 30;
			BaseAttack = 50;
			BaseGuard = 10;
			BaseSpeed = 28;

			Cooldown = 4;
			break;

		case (BONDSTATE.TWO):
			break;
		}
	}

	public class PunchyPunch : Skill {

		public override Participant USER { get; set;}

		public PunchyPunch() {
		}

		public override bool hasPriority ()
		{
			return false;
		}

		public override string getName() {
			return "PunchyPunch";
		}

		public override string getDescription() {
			return "A super strong attack";
		}

		public override void activate() {
			USER.TEAM.otherTeam.attack (USER.currentAttack * 2);
		}
	}

	private string STATUS;
	public override string ToString ()
	{
		return string.Format ("[FistMan, {0}]", STATUS);
	}
}
