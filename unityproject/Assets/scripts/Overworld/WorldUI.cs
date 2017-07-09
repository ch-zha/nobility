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
	private  MovePlayer MOVEMENT;

	public bool menuOn { get; set; }

	public void saveProgress() {
		GAMEDATA.currentScene = SceneManager.GetActiveScene ().name;
		Debug.Log (GAMEDATA.currentScene);
		GAMEDATA.Save ();
		hideMenu ();
	}

	public void showMenu() {
		PlayerMenu.SetActive (true);
		Misc.fadeIn (this, .05F, PlayerMenu.GetComponent<CanvasGroup> ());
		MOVEMENT.PLAYER_INPUT = false;
	}

	public void hideMenu() {
		PlayerMenu.SetActive (false);
		MOVEMENT.PLAYER_INPUT = true;
	}

	public void quit() {
		Application.Quit ();
	}

	// Use this for initialization
	void Start () {
		MOVEMENT = GameObject.Find ("Main Camera").GetComponent<MovePlayer> ();
		GAMEDATA = GameObject.Find ("GameData").GetComponent<GameData> ();
		PlayerMenu.SetActive (false);
		PlayerName.text = GAMEDATA.PLAYERNAME;
		HEALTH.text = System.Convert.ToString(GAMEDATA.currentTeam.teamHealth);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			showMenu ();
		}
	}
}
