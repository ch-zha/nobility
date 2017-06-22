using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParallax : MonoBehaviour {

	/*Controls camera through player input. Only supports horizontal motion, 
	 *but a vertical version may be desired?*/

	//MANUALS
	public float LEFTBOUND;
	public float RIGHTBOUND;
	public Vector3 CAMERA_SPEED = new Vector3(1, 0, 0);

	//GAME OBJECTS/COMPONENTS
	private Camera cam;

	//STATIC VARS
//	private Vector3 CAMERA_STARTINGPOS;

	//NONSTATIC VARS
	private float keydir;
	private Vector3 movement;
	private float cam_pos;

	/*Init*/
	void Start () {
		cam = this.gameObject.GetComponent<Camera> ();
//		CAMERA_STARTINGPOS = cam.transform.position;
	}

	/*Check for edge of world*/
	bool hitEdge(float keydir){
		cam_pos = cam.transform.position.x;

		if (cam_pos <= LEFTBOUND && keydir < 0) {
			return true;
		} else if (cam_pos >= RIGHTBOUND && keydir > 0) {
			return true;
		}
		return false;
	}
/*
	/*PARALLAX ZOOM (HOW IT WORK THO - FIGURE OUT LATER)
	public float GetFieldOfView(float orthoSize, float distanceFromOrigin)
	{
		// orthoSize
		float a = orthoSize;
		// distanceFromOrigin
		float b = Mathf.Abs(distanceFromOrigin);

		float fieldOfView = Mathf.Atan(a / b)  * Mathf.Rad2Deg * 2f;
		return fieldOfView;
	}

*/

	/* Update is called once per frame */
	void Update () {

		//update movement
		keydir = Input.GetAxis ("Horizontal");
		movement = Vector3.Scale(CAMERA_SPEED, new Vector3(keydir, 0, 0));
	}

	void FixedUpdate () {
		
		//check for edge of world
		if (hitEdge (keydir)) {
			return;
		}

		//move camera
		cam.transform.Translate (movement);
	}
}
