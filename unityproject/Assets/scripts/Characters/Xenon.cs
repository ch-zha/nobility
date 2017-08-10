using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Xenon : Character {

	public Xenon (BONDSTATE bondstate) {

		switch (bondstate) {
		case (BONDSTATE.ENEMY):

			STATUS = "ENEMY";

			BaseAttack = 50;
			BaseDefense = 50;
			BaseSpeed = 30;

			Skills = new Skill[] { new BasicAttack (BaseAttack), new BasicGuard(BaseDefense), new Heal (50), new Freeze (6) };
			break;


		case (BONDSTATE.ONE):

			STATUS = "BOND ONE";

			BaseAttack = 30;
			BaseDefense = 20;
			BaseSpeed = 20;

			Skills = new Skill[] { new ActionNone(), new BasicAttack(BaseAttack), new BasicGuard(BaseDefense), new SuperAttack (100), new Freeze (3) };
			break;
		}
	}
}