using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

	public string DIALOGUE;
	private Text TARGETBOX;

	private string str;
	private Button[] RESPONSES;

	IEnumerator AnimateText(string strComplete){
		int i = 0;
		str = "";
		while( i < strComplete.Length ) {
			Debug.Log(str + ";" + strComplete);
			str += strComplete[i++];
			yield return new WaitForSeconds(0.05F);
		}
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

	// Use this for initialization
	void OnEnable () {
		TARGETBOX = this.gameObject.GetComponentInChildren<Text> ();
		//RESPONSES = this.gameObject.GetComponentsInChildren<Button> ();
		StartCoroutine (AnimateText (DIALOGUE));
	}
	
	// Update is called once per frame
	void Update () {
		TARGETBOX.text = str;
	}
}
