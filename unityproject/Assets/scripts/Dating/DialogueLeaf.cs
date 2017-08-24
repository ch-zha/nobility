using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLeaf : MonoBehaviour {

	public string PROMPT;
	private Text TEXTBOX;
	public Button BUTT1;
	public Button BUTT2;
	public Button BUTT3;
	public Button BUTT4;
	public DialogueLeaf child1, child2, child3, child4;
	//private CanvasGroup AlphaControl;
	public bool wait;
	public int seconds;
	public bool isEnd;

	IEnumerator startTimer (int time) {
		yield return new WaitForSeconds (time); 
		//activate Child;
	}

	//destroy func

	// Use this for initialization
	void OnEnable () {
		TEXTBOX = GameObject.FindGameObjectWithTag ("Dialogue").GetComponent<Text>();
		TEXTBOX.text = PROMPT;
		if (wait) {
			StartCoroutine (startTimer (seconds));
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
