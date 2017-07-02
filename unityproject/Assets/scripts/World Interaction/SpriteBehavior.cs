using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBehavior : MonoBehaviour {

	private WorldUI UICONTROL;

	void Start() {
		UICONTROL = GameObject.Find ("Scripts").GetComponent<WorldUI> ();
	}

	void OnMouseDown() {
		UICONTROL.initDialogueOne();
	}

}
