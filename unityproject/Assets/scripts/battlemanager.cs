using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battlemanager : MonoBehaviour {

	public void endBattle() {
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
	}
}