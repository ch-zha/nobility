using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windblade : Character {

	public Windblade(int level) {
		switch (level) {
		case (1):
			BaseHealth = 100;
			BaseAttack = 20;
			BaseGuard = 20;

			Cooldown = 2;
			break;
		case (2):
			BaseHealth = 120;
			break;
		}
	}
}
