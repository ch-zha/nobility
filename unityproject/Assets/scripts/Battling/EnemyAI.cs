using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyAI {

	public static void makeDecision(Participant user) {
		int decision = UnityEngine.Random.Range (0, user.useableSkills.Length);
		if (!user.TEAM.exceedsCost (user.useableSkills [decision].cost)) {
			user.selected = user.useableSkills [decision];
			return;
		} else {
			makeDecision (user);
		}
	}
}
