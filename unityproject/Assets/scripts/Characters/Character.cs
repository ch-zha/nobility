using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Character {

	/*The abstract class Character is the base class 
	 * for all other character classes to extend from. 
	 * Will also store relationship and dialogue data 
	 * in the future.*/

	public enum BONDSTATE
	{
		ENEMY,
		ONE,
		TWO
	}

	/*STATS & PROPERTIES*/

	public string STATUS { get; set;}

	//public float BaseHealth {get; set; }
	public float BaseAttack { get; set; }
	public float BaseDefense { get; set; }
	public float BaseSpeed { get; set; }

	/*SKILLS*/
	public Skill[] Skills {get; set;}
}