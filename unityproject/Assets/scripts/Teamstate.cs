using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teamstate : MonoBehaviour {

	public static Teamstate teamstate;

	private float playerhealth;

	//add enum to handle all characters later

	// Use this for initialization
	void Awake () {
		if (teamstate == null) {
			DontDestroyOnLoad (this.gameObject);
			teamstate = this;
		} else if (teamstate != this) {
			Destroy (this);
		}
	}

	void Start () {
		//if game state present: import from game state
		//else import defaults from character files
		playerhealth = 100;
	}

	//Access variables
	public float getHealth (int character) {
		return playerhealth;
	}

	// Edit team state
	public void changeHealth (float health) {
		playerhealth += health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
