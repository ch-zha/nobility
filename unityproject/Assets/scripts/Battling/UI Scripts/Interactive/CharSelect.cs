using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharSelect : MonoBehaviour {

	public Vector3 cameraPoint;
	public Vector3 rotate;
	public CanvasGroup optionMenu;

	private CameraParallax CAMERA;
	private Toggle me;

	void Start() {
		me = gameObject.GetComponent<Toggle> ();
		CAMERA = GameObject.Find ("BattleCam").GetComponent<CameraParallax> ();
	}



	public void onSelect() {
		me.isOn = true;
	}

	public void selectChar() {

		if (me.isOn) {
			setCamera ();
		} else {
			deselect ();
		}
	}

	private void deselect() {
		me.isOn = false;
		closeMenu ();
		CAMERA.resetCamera ();
	}

	private void setCamera() {
		CAMERA.currentPoint = cameraPoint;
		CAMERA.rotate = rotate;
	}

	private void openMenu() {
		optionMenu.alpha = .7F;
		optionMenu.interactable = true;
		optionMenu.blocksRaycasts = true;
	}

	private void closeMenu() {
		optionMenu.alpha = 0;
		optionMenu.interactable = false;
		optionMenu.blocksRaycasts = false;
	}

	void Update() {
		if (Input.GetKeyDown ("a") && me.isOn) {
			openMenu ();
		}

		if (Input.GetKeyDown ("d") && me.isOn) {
			closeMenu ();
		}

		if (Input.GetKeyDown ("down") && me.isOn) {
			EventSystem.current.SetSelectedGameObject (null);
			deselect ();
		}

		if (Input.GetKeyDown ("up") && EventSystem.current.currentSelectedGameObject == null) {
			EventSystem.current.SetSelectedGameObject (EventSystem.current.firstSelectedGameObject);
		}
	}

}
