using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

		if (me.isOn) {
			optionMenu.alpha = .7F;
			optionMenu.interactable = true;
			optionMenu.blocksRaycasts = true;
			CAMERA.currentPoint = cameraPoint;
			CAMERA.rotate = rotate;
		} else {
			optionMenu.alpha = 0;
			optionMenu.interactable = false;
			optionMenu.blocksRaycasts = false;
			CAMERA.resetCamera ();
		}
	}

}
