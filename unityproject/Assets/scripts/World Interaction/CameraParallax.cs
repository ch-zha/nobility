using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParallax : MonoBehaviour {

	/*Controls camera through player input. Only supports horizontal motion, 
	 *but a vertical version may be desired?*/

	//MANUALS
	public float LEFTBOUND;
	public float RIGHTBOUND;
	public float SCROLL_SPEED = 1F;
	public float ZOOM_SPEED = .5F;

	//GAME OBJECTS/COMPONENTS
	private Camera cam;
	private Camera farCam;
	private Camera nearCam;

	//STATIC VARS
	private Vector3 CAMERA_STARTINGPOS;

	//NONSTATIC VARS
	private float keydir;
	private float keyzoom;
	private Vector3 movement;
	private float cam_pos;

	/*Init*/
	void Start () {
		cam = this.gameObject.GetComponent<Camera> ();
		nearCam = GameObject.Find("Parallax Near Camera").GetComponent<Camera> ();
		farCam = GameObject.Find("Parallax Far Camera").GetComponent<Camera> ();
		CAMERA_STARTINGPOS = cam.transform.position;
	}

	/*Check for edge of world*/
	bool hitEdge(float keydir){
		cam_pos = cam.transform.position.x;

		if (cam_pos <= CAMERA_STARTINGPOS.x - LEFTBOUND && keydir < 0) {
			return true;
		} else if (cam_pos >= CAMERA_STARTINGPOS.x + RIGHTBOUND && keydir > 0) {
			return true;
		}
		return false;
	}

	/*PARALLAX ZOOM*/
	public void GetFieldOfView() {

		//float a = cam.orthographicSize;
		float b = Mathf.Abs (cam.transform.position.z);

		//Set Ortho Zoom FOV
		//farCam.fieldOfView = Mathf.Atan(a/b) * Mathf.Rad2Deg * 2f;
		//nearCam.fieldOfView = farCam.fieldOfView;

		//Set Parallax Clipping Planes

		farCam.nearClipPlane = b;
		farCam.farClipPlane = cam.farClipPlane;
		nearCam.farClipPlane = b;
		nearCam.nearClipPlane = cam.nearClipPlane;
	}

	/* Update is called once per frame */
	void Update () {

		//update movement
		keydir = Input.GetAxis ("Horizontal");
		keyzoom = Input.GetAxis ("Vertical");
		movement = new Vector3(keydir*SCROLL_SPEED, 0, keyzoom * ZOOM_SPEED);
		cam.orthographicSize += - keyzoom * ZOOM_SPEED;
	}

	void FixedUpdate () {
		
		//check for edge of world
		if (hitEdge (keydir)) {
			return;
		}

		GetFieldOfView ();

		//move camera
		cam.transform.Translate (movement);
	}
}
