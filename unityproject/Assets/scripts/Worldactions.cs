using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worldactions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	public void initBattle() {
		//add switch cases if more than 1 battle?
		UnityEngine.SceneManagement.SceneManager.LoadScene(1);
	}

	public void die(GameObject me) {
		Destroy (me);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
