using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

	public Text INPUT;
	public GameObject ENTERNAME;
	public GameObject ERROR;
	public GameObject CONFIRMATION;
	private GameData GAMEFILE;

	private string PLAYERNAME;

	public void processName() {
		if (Input.GetButtonDown ("Submit")) {
			PLAYERNAME = INPUT.text;
			if (PLAYERNAME.Length > 2) {
				ERROR.SetActive (false);
				string msg = "So your name is " + PLAYERNAME + "?";
				ENTERNAME.SetActive (false);
				CONFIRMATION.GetComponent<Text> ().text = msg;
				CONFIRMATION.SetActive (true);
			} else {
				ERROR.SetActive (true);
			}
		}
	}

	public void editName() {
		CONFIRMATION.SetActive (false);
		ENTERNAME.SetActive (true);
	}

	public void confirmName() {
		GAMEFILE.PLAYERNAME = PLAYERNAME;
		GAMEFILE.Save ();
		SceneManager.LoadScene (3);
	}

	// Use this for initialization
	void Start () {
		GAMEFILE = GameObject.Find ("GameData").GetComponent<GameData> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
