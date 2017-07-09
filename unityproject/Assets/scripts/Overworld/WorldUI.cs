using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldUI : MonoBehaviour {

	/*Manages UI and player interface options in the open world.*/

	private GameData GAMEDATA;
	public Text HEALTH;
	public Text PlayerName;
	public GameObject PlayerMenu;

	public void saveProgress() {
		GAMEDATA.currentScene = SceneManager.GetActiveScene ().name;
		Debug.Log (GAMEDATA.currentScene);
		GAMEDATA.Save ();
		hideMenu ();
	}

	public void hideMenu() {
		PlayerMenu.SetActive (false);
	}

	public void quit() {
		Application.Quit ();
	}

	// Use this for initialization
	void Start () {
		GAMEDATA = GameObject.Find ("GameData").GetComponent<GameData> ();
		PlayerName.text = GAMEDATA.PLAYERNAME;
		HEALTH.text = System.Convert.ToString(GAMEDATA.currentTeam.teamHealth);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			PlayerMenu.SetActive (true);
		}
	}
}
