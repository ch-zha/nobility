using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

	public string DIALOGUE;
	private Text TARGETBOX;
	private CanvasGroup AlphaControl;

	private string str;
	private Button[] RESPONSES;

	public bool textComplete;

	IEnumerator fadeIn() {
		while (AlphaControl.alpha < 1) {
			AlphaControl.alpha += .05F;
			yield return new WaitForFixedUpdate ();
		}
		StartCoroutine (AnimateText (DIALOGUE));
	}

	IEnumerator AnimateText(string strComplete){
		int i = 0;
		str = "";
		while( i < strComplete.Length ) {
			//Debug.Log(str + ";" + strComplete);
			str += strComplete[i++];
			yield return new WaitForSeconds(0.05F);
		}
		textComplete = true;
	}

	/*
	private void activateResponse() {
		if (str == DIALOGUE) {
			foreach (Button response in RESPONSES) {
				response.enabled = true;
			}
		} else {
			foreach (Button response in RESPONSES) {
				response.enabled = false;
			}
		}
	}
	*/

	void Awake() {

	}

	// Use this for initialization
	void OnEnable () {
		str = "";
		textComplete = false;
		TARGETBOX = this.gameObject.GetComponentInChildren<Text> ();
		AlphaControl = this.gameObject.GetComponent<CanvasGroup> ();
		AlphaControl.alpha = 0;
		//RESPONSES = this.gameObject.GetComponentsInChildren<Button> ();
		StartCoroutine (fadeIn());
	}
	
	// Update is called once per frame
	void Update () {
		TARGETBOX.text = str;
	}
}
