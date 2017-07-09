using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOptions : MonoBehaviour {

	private BattleLoad LOAD;

	private Button ENDTURN;

	private GameObject P1;
	private GameObject P2;
	private GameObject P3;
	private GameObject[] PLAYERS;

	private GameObject P1OPTIONMENU;
	private GameObject P2OPTIONMENU;
	private GameObject P3OPTIONMENU;
	private GameObject[] OPTIONMENUS;

	private Option[] P1OPTIONS;
	private Option[] P2OPTIONS;
	private Option[] P3OPTIONS;
	private Option[][] PLAYEROPTIONS;

	private ToggleGroup P1GROUP;
	private ToggleGroup P2GROUP;
	private ToggleGroup P3GROUP;
	private ToggleGroup[] TOGGLEGROUPS;

	// Use this for initialization
	void Awake () {
		getUIComponents ();
	}

	private void getCharacterInfo() {
	}

	private void getUIComponents() {

		LOAD = GameObject.Find ("Scripts").GetComponent<BattleLoad> ();

		ENDTURN = GameObject.Find ("EndTurn").GetComponent<Button> ();

		if (LOAD == null) {
			Debug.Log ("Current battle not found");
		}

		P1 = GameObject.Find ("p1");
		P2 = GameObject.Find ("p2");
		P3 = GameObject.Find ("p3");
		PLAYERS = new GameObject[] { P1, P2, P3 };

		P1OPTIONMENU = GameObject.Find ("p1options");
		P2OPTIONMENU = GameObject.Find ("p2options");
		P3OPTIONMENU = GameObject.Find ("p3options");
		OPTIONMENUS = new GameObject[] { P1OPTIONMENU, P2OPTIONMENU, P3OPTIONMENU };

		P1OPTIONS = new Option[3];
		P2OPTIONS = new Option[3];
		P3OPTIONS = new Option[3];
		PLAYEROPTIONS = new Option[][] {P1OPTIONS, P2OPTIONS, P3OPTIONS};

		P1GROUP = P1OPTIONMENU.GetComponent<ToggleGroup>();
		P2GROUP = P2OPTIONMENU.GetComponent<ToggleGroup>();
		P3GROUP = P3OPTIONMENU.GetComponent<ToggleGroup>();
		TOGGLEGROUPS = GameObject.Find ("Team").GetComponentsInChildren<ToggleGroup> ();

		if (OPTIONMENUS == null) {
			Debug.Log ("UI elements missing");
		}

		for (int i = 0; i < OPTIONMENUS.Length; i++) {
			getOptions (OPTIONMENUS[i], PLAYEROPTIONS[i], i);
		}

		Debug.Log ("Player icons:" + System.Convert.ToString (PLAYERS[0]) + ", " + System.Convert.ToString (PLAYERS[1]) + ", " + System.Convert.ToString (PLAYERS[2]));
		Debug.Log ("Player option menus:" + System.Convert.ToString (OPTIONMENUS[0]) + ", " + System.Convert.ToString (OPTIONMENUS[1]) + ", " + System.Convert.ToString (OPTIONMENUS[2]));
		Debug.Log ("Player option arrays:" + System.Convert.ToString (PLAYEROPTIONS[0]) + ", " + System.Convert.ToString (PLAYEROPTIONS[1]) + ", " + System.Convert.ToString (PLAYEROPTIONS[2]));
	}

	private void getOptions (GameObject optionmenu, Option[] playeroptions, int i) {
		Option[] options = optionmenu.GetComponentsInChildren<Option> ();
		if (options.Length != 3) {
			Debug.Log ("Improper number of actions for Character" + System.Convert.ToString (i+1));
		} else {
			for (int x = 0; x < options.Length; x++) {
				playeroptions [x] = options [x];
			}
		}
	}

	public void updateOption() {
		for (int i = 0; i < LOAD.TEAM.TEAMMATES.Length; i++) {
			if (LOAD.TEAM.TEAMMATES [i] != null) {
				bool optionSelected = false;
				foreach (Option option in PLAYEROPTIONS[i]) {
					if (option.selected == true) {
						LOAD.TEAM.TEAMMATES [i].selected = option.action;
						optionSelected = true;
						//Debug.Log ("Character action set to" + System.Convert.ToString (option.action));
					}
				}

				if (optionSelected == false) {
					LOAD.TEAM.TEAMMATES [i].selected = Participant.Action.NONE;
					//Debug.Log ("No action selected for Character" + System.Convert.ToString (i + 1));
				}

			} else {
				Debug.Log ("Character" + System.Convert.ToString(i+1) + "not found");
			}
		}
	}

	public void cooldownDisable() {
		for (int i = 0; i < PLAYEROPTIONS.Length; i++) {
			foreach (Option option in PLAYEROPTIONS[i]) {
				if (option.action == Participant.Action.SKILL) {
					if (LOAD.TEAM.TEAMMATES [i].skillReady()) {
						option.toggle.enabled = true;
					} else {
						option.toggle.enabled = false;
					}
				}
			}
		}
	}

	public void onClick() {
		Debug.Log ("Clicked");
	}

	public void resetOptions() {
		foreach (ToggleGroup togglegroup in TOGGLEGROUPS) {
			togglegroup.SetAllTogglesOff();
		}
	}

	public void toggleOn(bool mode) {
		GameObject.Find ("Team").GetComponent<ToggleGroup> ().SetAllTogglesOff ();
		foreach (GameObject player in PLAYERS) {
			player.GetComponent<Toggle> ().enabled = mode;
		}
	}

	public void playerMenuToggle() {
		for (int i = 0; i < PLAYERS.Length; i++) {
			if (PLAYERS [i].GetComponent<Toggle> ().isOn) {
				OPTIONMENUS [i].GetComponent<CanvasGroup>().alpha = 1;
				OPTIONMENUS [i].GetComponent<CanvasGroup> ().interactable = true;
				OPTIONMENUS [i].GetComponent<CanvasGroup> ().blocksRaycasts = true;
			} else {
				OPTIONMENUS [i].GetComponent<CanvasGroup>().alpha = 0;
				OPTIONMENUS [i].GetComponent<CanvasGroup> ().interactable = false;
				OPTIONMENUS [i].GetComponent<CanvasGroup> ().blocksRaycasts = false;
			}
		}
	}

	public void goToNext() {
		bool p1set = P1GROUP.AnyTogglesOn ();
		bool p2set = P2GROUP.AnyTogglesOn ();
		bool p3set = P3GROUP.AnyTogglesOn ();

		if (p1set && p2set && p3set) {
			Debug.Log ("all set");
		} else if (!p1set) {
			P1.GetComponent<Toggle> ().isOn = true;
		} else if (!p2set) {
			P2.GetComponent<Toggle> ().isOn = true;
		} else if (!p3set) {
			P3.GetComponent<Toggle> ().isOn = true;
		} else {
			Debug.Log("please help something is terribly wrong");
		}
	}

	void Start() {
		playerMenuToggle ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
