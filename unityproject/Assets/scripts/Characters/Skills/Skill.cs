using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class Skill {

	/*Base class for all basic and character-specific skills. Still 
	 * debating whether to use it primarily for inheriting or for 
	 * instancing.*/

	public enum SKILLTYPE
	{
		NONE,
		OFFENSE,
		DEFENSE,
		OTHER
	}

	/*Use skill*/
	public abstract Participant user { get; set;}
	public abstract void activate (TeamStatus self, TeamStatus enemy);

	/*Flexible qualities*/
	public abstract int cost { get; set;}

	/*Static qualities*/
	public abstract bool hasPriority ();
	public abstract string getName ();
	public abstract SKILLTYPE getType ();
	public abstract string getEvent ();
	public abstract string getDescription ();

}
