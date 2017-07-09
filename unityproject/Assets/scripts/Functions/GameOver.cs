using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	private GameData GAMEDATA;

	void tryAgain() {
		if (GAMEDATA.currentScene != null) {
			SceneManager.LoadScene (GAMEDATA.currentScene);
		} else {
			SceneManager.LoadScene (0);
		}
	}

	void quit() {
		Application.Quit ();
	}

	// Use this for initialization
	void Start () {
		GAMEDATA = GameObject.Find ("GameData").GetComponent<GameData> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
