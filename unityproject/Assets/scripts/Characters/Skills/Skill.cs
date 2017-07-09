using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class Skill {

	/*Base class for all basic and character-specific skills. Still 
	 * debating whether to use it primarily for inheriting or for 
	 * instancing.*/

	public abstract Participant USER { get; set;}

	/*Use skill*/
	public abstract void activate ();
	public abstract bool hasPriority();
	public abstract string getName ();
	public abstract string getDescription ();

}
