using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCoroutines {

	private BattleUI DISPLAY;
	private List<IEnumerator> AnimationQueue;

	public bool animationsOver {get; set;}

	public BattleCoroutines(BattleUI display) {
		DISPLAY = display;
		animationsOver = true;
		AnimationQueue = new List<IEnumerator>();
	}

	/*MANAGE ANIMATIONQUEUE*/
	public void addAnimation(IEnumerator animation) {
		AnimationQueue.Add (animation);
	}

	private void runNext() {
		if (AnimationQueue.Count > 0) {
			DISPLAY.StartCoroutine (AnimationQueue [0]);
			AnimationQueue.RemoveAt (0);
		} else if (AnimationQueue.Count == 0) {
			animationsOver = true;
		}
	}

	public void runAnimationQueue() {
		animationsOver = false;
		runNext ();
	}

	/*COROUTINES*/
	public IEnumerator wait() {
		Debug.Log ("Someone did something");
		yield return new WaitForSeconds(.5F);
		runNext ();
	}

	public IEnumerator waitForHealth(UISnapshot snapshot) {
		Debug.Log ("Someone did something big");
		DISPLAY.updateUIHealth (snapshot);
		while (!DISPLAY.healthUpdated ()) {
			yield return new WaitForSeconds(.1F);
		}
		yield return new WaitForSeconds(1F);
		runNext ();
	}

	public class UISnapshot {

		public float playerHealth;
		public float playerMaxHealth;

		public float enemyHealth;
		public float enemyMaxHealth;

		public UISnapshot (BattleLoad snapshot) {

			playerHealth = snapshot.TEAM.teamHealth;
			playerMaxHealth = snapshot.TEAM.teamMaxHealth;
			enemyHealth = snapshot.ENEMY.teamHealth;
			enemyMaxHealth = snapshot.ENEMY.teamMaxHealth;

			Debug.Log("Snapped!" + this.ToString());
		}

		public override string ToString ()
		{
			return string.Format ("[UISnapshot: {0}/{1}, {2}/{3}]", playerHealth, playerMaxHealth, enemyHealth, enemyMaxHealth);
		}
	}

}
