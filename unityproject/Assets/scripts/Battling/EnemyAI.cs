using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyAI {

	public static int teammateOneDecision(Participant user) {
		//1 is Attack, 2 is Guard, 3, is Skill
		if (user.skillReady () && user.TEAM.teamMaxHealth - user.TEAM.teamHealth > 50) {
			return 3;
		} else {
			return UnityEngine.Random.Range (1, 3);
		}
	}

	public static int teammateTwoDecision(Participant user) {
		return UnityEngine.Random.Range (1, 3);
	}

	/*xenon is a dumb*/
}
