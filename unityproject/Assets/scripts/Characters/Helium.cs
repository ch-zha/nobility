using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helium : Character {

	public Helium (BONDSTATE bondstate) {
		Skill = new BalloonMan();

		switch (bondstate) {
		case (BONDSTATE.ENEMY):

			STATUS = "ENEMY";

			BaseHealth = 280;
			BaseAttack = 30;
			BaseGuard = 20;
			BaseSpeed = 11;
			Cooldown = 3;
			break;

		case (BONDSTATE.ONE):

			STATUS = "BOND ONE";

			BaseHealth = 60;
			BaseAttack = 25;
			BaseGuard = 20;
			BaseSpeed = 11;

			Cooldown = 3;
			break;

		case (BONDSTATE.TWO):

			STATUS = "BOND TWO";

			BaseHealth = 90;
			BaseAttack = 30;
			BaseGuard = 25;
			BaseSpeed = 11;

			Cooldown = 2;
			break;
		}
	}

	public class BalloonMan : Skill {

		public BalloonMan() {
		}

		public override bool hasPriority ()
		{
			return true;
		}

		public override string getName() {
			return "Balloon Man";
		}

		public override string getDescription() {
			return "Increases team speed by 2";
		}

		public override void activate(TeamStatus team) {
			team.createNewStatus(TeamStatus.StatusEffect.EFFECT.SPEED, 2, 2);
		}
	}
		
	private string STATUS;
	public override string ToString ()
	{
		return string.Format ("[Helium, {0}]", STATUS);
	}
}