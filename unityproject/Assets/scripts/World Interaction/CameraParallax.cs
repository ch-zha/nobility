using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParallax : MonoBehaviour {

	/*Controls camera through player input. Only supports horizontal motion, 
	 *but a vertical version may be desired?*/

	//MANUALS
	public bool PLAYER_INPUT = true;

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
	private float dir;
	private float zoom;
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
	private bool hitEdge(float dir){
		cam_pos = cam.transform.position.x;

		if (cam_pos <= CAMERA_STARTINGPOS.x - LEFTBOUND && dir < 0) {
			return true;
		} else if (cam_pos >= CAMERA_STARTINGPOS.x + RIGHTBOUND && dir > 0) {
			return true;
		}
		return false;
	}

	/*PARALLAX ZOOM*/
	private void GetFieldOfView() {

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

	private Vector3 getMovement(bool input) {
		if (input == true) {
			dir = Input.GetAxis ("Horizontal");
			zoom = Input.GetAxis ("Vertical");
		}

		if (hitEdge(dir)) {
			return new Vector3(0, 0, 0);
		}

		cam.orthographicSize += - zoom * ZOOM_SPEED;
		return new Vector3(dir*SCROLL_SPEED, 0, zoom * ZOOM_SPEED);
	}

	/* Update is called once per frame */
	void Update () {
	}

	void FixedUpdate () {

		GetFieldOfView ();
		cam.transform.Translate (getMovement(PLAYER_INPUT));
	}
}
