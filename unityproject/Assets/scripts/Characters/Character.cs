﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {

	/*The abstract class Character is the base class 
	 * for all other character classes to extend from. 
	 * Will also store relationship and dialogue data 
	 * in the future.*/

	/*STATIC PROPERTIES*/
	public float BaseHealth {get; set; }
	public float BaseAttack { get; set;}
	public float BaseGuard { get; set;}

	public int Cooldown { get; set;}
	public Skill CharSkill;

	/*SKILLS*/

	void Start () {
	}
}