using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class StartMenu: MonoBehaviour {

	public GameData GAMEDATA;

	public GameObject LOGO;
	private CanvasGroup LOGO_CANVAS;
	public GameObject NEWGAME;
	private CanvasGroup NEWGAME_CANVAS;
	public GameObject CONTINUE;
	private CanvasGroup CONTINUE_CANVAS;


	private bool detectSave() {		
		if (File.Exists (Application.persistentDataPath + "/savedata.dat")) {
			return true;
		}
		return false;
	}

	IEnumerator fadeIn (CanvasGroup objectOne, CanvasGroup objectTwo) {
		while (objectOne.alpha < 1) {
			objectOne.alpha += .01F;
			yield return new WaitForFixedUpdate();
		}

		if (detectSave()) {
			while (CONTINUE_CANVAS.alpha < 1) {
				CONTINUE_CANVAS.alpha += .05F;
				yield return new WaitForFixedUpdate ();
			}
		}

		while (objectTwo.alpha < 1) {
			objectTwo.alpha += .05F;
			yield return new WaitForFixedUpdate();
		}

	}

	public void newGame() {
		SceneManager.LoadScene (1);
	}

	public void continueGame() {
		GAMEDATA.Load ();
		if (GAMEDATA.currentScene != null) {
			SceneManager.LoadScene (GAMEDATA.currentScene);
		} else {
			Debug.Log ("Error: Save File Invalid"); 
		}
	}

	void Awake() {
		LOGO_CANVAS = LOGO.GetComponent<CanvasGroup> ();
		LOGO_CANVAS.alpha = 0;

		NEWGAME_CANVAS = NEWGAME.GetComponent<CanvasGroup> ();
		NEWGAME_CANVAS.alpha = 0;

		CONTINUE_CANVAS = CONTINUE.GetComponent<CanvasGroup> ();
		CONTINUE_CANVAS.alpha = 0;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (Misc.fadeIn(this, .01F, LOGO_CANVAS, 
			Misc.fadeIn(this, .05F, CONTINUE_CANVAS, 
				Misc.fadeIn(this, .05F, NEWGAME_CANVAS))));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
