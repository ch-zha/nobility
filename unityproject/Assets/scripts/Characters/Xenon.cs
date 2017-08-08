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

			BaseAttack = 80;
			BaseDefense = 40;
			BaseSpeed = 40;
			break;

		case (BONDSTATE.ONE):

			STATUS = "BOND ONE";

			BaseAttack = 20;
			BaseDefense = 20;
			BaseSpeed = 20;
			break;
		}
	}
}