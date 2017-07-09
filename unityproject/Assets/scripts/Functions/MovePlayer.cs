using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour {

	//MANUALS
	public bool PLAYER_INPUT = true;

	public float LEFTBOUND;
	public float RIGHTBOUND;
	public float INBOUND = 2;
	public float OUTBOUND = 0;
	public float SCROLL_SPEED = 1F;
	public float ZOOM_SPEED = .5F;

	//GAME OBJECTS/COMPONENTS
	private GameObject me;
	private Camera cam;
	private Camera farCam;
	private Camera nearCam;

	//STATIC VARS
	private Vector3 STARTINGPOS;

	//NONSTATIC VARS
	private float dir;
	private float zoom;
	private Vector3 movement;

	/*Init*/
	void Awake () {
		me = GameObject.Find("Hydrogen");

		cam = this.gameObject.GetComponent<Camera> ();
		nearCam = GameObject.Find("Parallax Near Camera").GetComponent<Camera> ();
		farCam = GameObject.Find("Parallax Far Camera").GetComponent<Camera> ();
		STARTINGPOS = me.transform.position;
	}

	void Start() {

		cam.transform.position = new Vector3(me.transform.position.x, cam.transform.position.y, cam.transform.position.z);
	}

	/*Check for edge of world*/
	private bool hitEdge(float dir, float zoom){
		float me_pos = me.transform.position.x;
		float me_zoom = me.transform.position.z;

		if (me_pos <= STARTINGPOS.x - LEFTBOUND && dir < 0) {
			return true;
		} else if (me_pos >= STARTINGPOS.x + RIGHTBOUND && dir > 0) {
			return true;
		} else if (me_zoom >= STARTINGPOS.z + INBOUND && zoom > 0) {
			return true;
		} else if (me_zoom <= STARTINGPOS.z - OUTBOUND && zoom < 0) {
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

		if (hitEdge(dir, zoom)) {
			return new Vector3(0, 0, 0);
		}

		//cam.orthographicSize += - zoom * ZOOM_SPEED;
		return new Vector3(dir*SCROLL_SPEED, 0, zoom * ZOOM_SPEED);
	}

	IEnumerator moveTo(Vector3 target) {
		if (cam.transform.position != target) {
			float i = 0;
			while (i < 1) {
				i += 0.001F;
				cam.transform.position = Vector3.Lerp (cam.transform.position, target, i);
				yield return new WaitForFixedUpdate ();
			}
		} else {
			yield break;
		}
	}

	/* Update is called once per frame */
	void Update () {
		GetFieldOfView ();

		if (dir > 0) {
			GameObject.Find ("HydrogenSprite").transform.localEulerAngles = new Vector3 (0, 180, 0);
		} else if (dir < 0) {
			GameObject.Find ("HydrogenSprite").transform.localEulerAngles = new Vector3 (0, 0, 0);
		}

		if (PLAYER_INPUT) {
			me.transform.Translate (getMovement (PLAYER_INPUT));
			if ((me.transform.position.x <= STARTINGPOS.x + RIGHTBOUND - cam.orthographicSize) && 
				(me.transform.position.x >= STARTINGPOS.x - LEFTBOUND + cam.orthographicSize)) {
				cam.transform.position = new Vector3(me.transform.position.x, cam.transform.position.y, cam.transform.position.z);
			}
		}
	}
}
