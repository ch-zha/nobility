using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	/*The abstract class Character is the base class 
	 * for all other character classes to extend from. 
	 * Will also store relationship and dialogue data 
	 * in the future.*/

	/*STATIC PROPERTIES*/
	public float MaxHealth { get; set; }
	public float BaseAttack { get; set; }
	public float BaseDefense { get; set; }

	/*SKILLS*/


	/*CASE HANDLING FOR LEVELS*/


	void Start() {
	}

	void Update() {
	}

}