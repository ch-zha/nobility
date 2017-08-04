using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharSelect : MonoBehaviour {

	public Vector3 cameraPoint;
	public Vector3 rotate;

	public CanvasGroup optionMenu;

	private CharNav NAVCONTROL;
	private CameraParallax CAMERA;

	private bool menuOpen { get; set;}
	public static bool coroutineOneRunning { get; set;}
	public static bool coroutineTwoRunning { get; set;}


	public void clickSelect() {
		if (!coroutineOneRunning && !coroutineTwoRunning) {
			NAVCONTROL.currentAlly.closeMenu ();
			NAVCONTROL.currentAlly = this;
			selectchar ();
		}
	}

	public void selectchar() {
		setCamera ();
		StartCoroutine (startCharSelect ());
	}

	public void deselect() {
		closeMenu ();
		CAMERA.resetCamera ();
		EventSystem.current.SetSelectedGameObject (null);
	}

	public void setCamera() {
		CAMERA.currentPoint = cameraPoint;
		CAMERA.rotate = rotate;
	}

	public void openMenu() {
		optionMenu.alpha = .7F;
		optionMenu.interactable = true;
		optionMenu.blocksRaycasts = true;
		menuOpen = true;
		EventSystem.current.SetSelectedGameObject (optionMenu.gameObject.GetComponent<MenuOption>().options[0].gameObject);
	}

	public void closeMenu() {
		optionMenu.alpha = 0;
		optionMenu.interactable = false;
		optionMenu.blocksRaycasts = false;
		menuOpen = false;
	}

	IEnumerator startCharSelect() {
		coroutineTwoRunning = true;
		yield return new WaitForSeconds (.5F);
		openMenu ();
		coroutineTwoRunning = false;
	}

	IEnumerator endCharSelect() {
		coroutineOneRunning = true;
		yield return new WaitForSeconds (.5F);
		closeMenu ();
		NAVCONTROL.goToNext ();
		coroutineOneRunning = false;
	}


	/*RUNTIME*/

	void Awake() {
		closeMenu ();
		coroutineOneRunning = false;
		coroutineTwoRunning = false;
		menuOpen = false;

		NAVCONTROL = GameObject.Find ("BattleScripts").GetComponent<CharNav> ();
		CAMERA = GameObject.Find ("BattleCam").GetComponent<CameraParallax> ();

		if (CAMERA == null) {
			Debug.Log ("Camera Not Found");
		}
	}
		
	void Update() {

		if (!coroutineOneRunning && !coroutineTwoRunning) {
			/*
			if (Input.GetKeyDown ("q") && NAVCONTROL.currentAlly == this) {
				if (!menuOpen) {
					openMenu ();
				} else {
					closeMenu ();
				}
			}
			*/

			if (Input.GetKeyDown ("e") && NAVCONTROL.currentAlly == this) {
				EventSystem.current.currentSelectedGameObject.GetComponent<Toggle> ().isOn = true;
				StartCoroutine (endCharSelect ());
			}
		}
	}

}
