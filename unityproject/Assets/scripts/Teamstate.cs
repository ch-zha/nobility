using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teamstate : MonoBehaviour {

	public static Teamstate teamstate;

	public Teammate player;

	/*TEAMMATE CONSTRUCTOR FOR TEMP TEAMMATE ELEMENTS*/
	public class Teammate {

		public Teammate() {
			Health = 100;
		}

		public float Health { get; set; }
	}

	//NOTE: add enum to handle all characters later?

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
		player = new Teammate();
	}

	// Edit team state
	public void changeHealth (float health) {
		player.Health += health;
		if (player.Health <= 0) {
			player.Health = 0;
			//call death
		} else if (player.Health > 100) /*change condition to char's max health*/ {
			player.Health = 100;
		}
	}


	void Update () {
		
	}
}
