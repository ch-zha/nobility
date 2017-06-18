using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windblade : Character {

	public Windblade(int level) {
		switch (level) {
		case (1):
			MaxHealth = 100;
			break;
		case (2):
			MaxHealth = 120;
			break;
		}
	}
}
