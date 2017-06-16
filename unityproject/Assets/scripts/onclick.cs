using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onclick : MonoBehaviour {

	private GameObject me;

	// Use this for initialization
	void Start () {
		me = this.gameObject;
	}

	void OnMouseDown() {
		Destroy (me);
	}

	// Update is called once per frame
	void Update () {
}
}