using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill {

	/*Base class for all basic and character-specific skills. Still 
	 * debating whether to use it primarily for inheriting or for 
	 * instancing.*/

	public int Cooldown { get; private set; }

	/*LEVEL HANDLING*/

	/*Use skill*/
	public void activate () {}

	/*BATTLE FUNCTIONS*/

}
