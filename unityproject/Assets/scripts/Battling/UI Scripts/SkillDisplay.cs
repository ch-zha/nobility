using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDisplay : MonoBehaviour {

	public int CharacterNumber;

	private BattleLoad LOAD;

	private Text me;
	private string skillname;

	// Use this for initialization
	void Start () {
		LOAD = GameObject.Find ("Scripts").GetComponent<BattleLoad> ();

		me = this.gameObject.GetComponent<Text>();
		skillname = LOAD.TEAM.TEAMMATES [CharacterNumber].skill.getName();
	}
	
	// Update is called once per frame
	void Update () {
		int cooldown = LOAD.TEAM.TEAMMATES [CharacterNumber].cooldown;
		if (cooldown > 0) {
			me.text = skillname + ": in " + System.Convert.ToString (LOAD.TEAM.TEAMMATES [CharacterNumber].cooldown);
		} else if (cooldown == 0) {
			me.text = skillname + ": READY";
		}
	}
}
