using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill {

	/*Base class for all basic and character-specific skills. Still 
	 * debating whether to use it primarily for inheriting or for 
	 * instancing.*/

	/*LEVEL HANDLING*/

	/*Use skill*/
	public abstract void activate (TeamStatus side);
	public abstract string getName ();
	public abstract string getDescription ();

}
