using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyAI {

	public static int randomDecision(Participant user) {
		//1 is Attack, 2 is Guard, 3, is Skill
		if (user.skillReady ()) {
			return UnityEngine.Random.Range (1, 4);
		} else {
			return UnityEngine.Random.Range (1, 3);
		}
	}

}
