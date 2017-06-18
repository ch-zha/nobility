using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Basic {

	public static void attack(Participant user, Participant target) {
		target.currentHealth -= user.currentAttack;
		if (target.currentHealth <= 0) {
			target.currentHealth = 0;
			target.dead = true;
		}
	}
}
