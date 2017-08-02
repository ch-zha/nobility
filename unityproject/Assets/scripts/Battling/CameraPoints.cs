using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoints : MonoBehaviour {

	/*RETIRED CLASS DO NOT USE*/

	public Vector3 defaultPoint = new Vector3 (0, 0, -10);
	public Vector3 enemyOne = new Vector3 (-10, 3, -2);
	public Vector3 enemyTwo;
	public Vector3 enemyThree;
	public Vector3 playerOne;
	public Vector3 playerTwo;
	public Vector3 playerThree;

	public Vector3 origPoint;
	public Vector3 currentPoint;

	void Start () {
		
	}

	public void changeCurrentPoint(Vector3 target) {
		currentPoint = target;
	}

	public void resetPoint () {
		currentPoint = origPoint;
	}

}
