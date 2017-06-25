using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xenon : Character {

	public Xenon (BONDSTATE bondstate) {
		Skill = new AnimeBoy();

		switch (bondstate) {
		case (BONDSTATE.ENEMY):
			BaseHealth = 280;
			BaseAttack = 30;
			BaseGuard = 20;
			Cooldown = 3;
			break;

		case (BONDSTATE.ONE):
			BaseHealth = 80;
			BaseAttack = 20;
			BaseGuard = 20;

			Cooldown = 4;
			break;

		case (BONDSTATE.TWO):
			BaseHealth = 90;
			BaseAttack = 30;
			BaseGuard = 25;

			Cooldown = 3;
			break;
		}
	}

	public class AnimeBoy : Skill {

		public AnimeBoy() {}

		public override string getName() {
			return "Anime Boy";
		}

		public override string getDescription() {
			return "Heals team by 50";
		}

		public override void activate(TeamStatus team) {
			team.heal (50);
		}
	}
		
	public override string ToString ()
	{
		return string.Format ("[Xenon]");
	}
}