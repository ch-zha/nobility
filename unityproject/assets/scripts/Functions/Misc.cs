using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Misc  {

	public static IEnumerator fadeIn(MonoBehaviour mono, float speed, CanvasGroup canvas) {
		mono.StartCoroutine(fadeIn (mono, speed, canvas, null));
		yield break;
	}

	public static IEnumerator fadeIn(MonoBehaviour mono, float speed, CanvasGroup canvas, IEnumerator next) {
		while (canvas.alpha < 1) {
			canvas.alpha += speed;
			yield return new WaitForFixedUpdate ();
		}
		if (next != null) {
			mono.StartCoroutine (next);
		}
	}

}
