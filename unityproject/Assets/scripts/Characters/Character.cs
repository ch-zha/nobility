using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {

	/*The abstract class Character is the base class 
	 * for all other character classes to extend from. 
	 * Will also store relationship and dialogue data 
	 * in the future.*/

	/*STATIC PROPERTIES*/
	public float MaxHealth {get; set; }
	public float BaseAttack { get; set;}
	//abstract public float BaseDefense { get; }
	public Skill CharSkill;

	/*SKILLS*/

	void Start () {
	}
}