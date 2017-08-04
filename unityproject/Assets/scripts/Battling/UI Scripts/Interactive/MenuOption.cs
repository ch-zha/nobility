using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class MenuOption : MonoBehaviour {

	public Selectable[] options {get; set;}

	void Start () {
		options = this.gameObject.GetComponentsInChildren<Selectable> ();
	}

	// Update is called once per frame
	void Update () {
	}
}
