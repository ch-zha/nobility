using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneOne : MonoBehaviour {

	private GameObject[] DIALOGUES;
	private GameObject ONE;
	private GameObject TWO;

	void OnTriggerEnter2D (Collider2D col) {
		Debug.Log ("I see you binch");
		initDialogues ();
	}

	void OnTriggerExit2D (Collider2D col) {
		exitDialogue ();
	}

	void OnMouseDown () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}

	IEnumerator wait() {
		while (DIALOGUES [0].GetComponent<Dialogue> ().textComplete == false) {
			yield return new WaitForFixedUpdate ();
		}
		DIALOGUES [1].SetActive(true);
	}

	public void initDialogues() {
		DIALOGUES [0].SetActive(true);
		StartCoroutine (wait ());
	}

	public void exitDialogue() {
		foreach (GameObject dialogue in DIALOGUES) {
			dialogue.SetActive (false);
		}
		//CONTROLS.enabled = true;
	}

	void Awake() {
		ONE = GameObject.Find("DialogueOne");
		TWO = GameObject.Find ("DialogueTwo");
		DIALOGUES = new GameObject[] { ONE, TWO };
		exitDialogue ();
	}

	void Update() {
	}
}
