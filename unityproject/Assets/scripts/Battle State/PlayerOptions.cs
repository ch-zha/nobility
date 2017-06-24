using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOptions : MonoBehaviour {

	private GameObject p1;
	private GameObject p2;
	private GameObject p3;

	private GameObject p1options;
	private GameObject p2options;
	private GameObject p3options;

	// Use this for initialization
	void Awake () {
		p1 = GameObject.Find ("p1");
		p2 = GameObject.Find ("p2");
		p3 = GameObject.Find ("p3");

		p1options = GameObject.Find ("p1options");
		p2options = GameObject.Find ("p2options");
		p3options = GameObject.Find ("p3options");

		p1options.SetActive (false);
		p2options.SetActive (false);
		p3options.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (p1.GetComponent<Toggle> ().isOn) {
			p1options.SetActive (true);
		} else {
			p1options.SetActive (false);
		}

		if (p2.GetComponent<Toggle> ().isOn) {
			p2options.SetActive (true);
		} else {
			p2options.SetActive (false);
		}

		if (p3.GetComponent<Toggle> ().isOn) {
			p3options.SetActive (true);
		} else {
			p3options.SetActive (false);
		}
	}
}
