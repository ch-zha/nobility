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

	private void clearQueue() {
		AnimationQueue = new List<IEnumerator>();
	}

	public void runAnimationQueue() {
		animationsOver = false;
		runNext ();
	}

	/*COROUTINES*/
	public IEnumerator wait(string description) {
		Debug.Log ("Someone did something");
		DISPLAY.ACTIONDESC.text = description;
		yield return new WaitForSeconds(2F);
		runNext ();
	}

	public IEnumerator waitForHealth(UISnapshot snapshot) {
		DISPLAY.updateUIHealth (snapshot);
		DISPLAY.ACTIONDESC.text = snapshot.snapshotDescription;
		while (!DISPLAY.healthUpdated ()) {
			yield return new WaitForSeconds(.1F);
		}
		yield return new WaitForSeconds(1F);

		if (snapshot.enemyHealth == 0 || snapshot.playerHealth == 0) {
			clearQueue();
		}

		runNext ();
	}

	public class UISnapshot {

		public float playerHealth;
		public float playerMaxHealth;

		public float enemyHealth;
		public float enemyMaxHealth;

		public string snapshotDescription;

		public UISnapshot (BattleLoad snapshot, string description) {

			playerHealth = snapshot.TEAM.teamHealth;
			playerMaxHealth = snapshot.TEAM.teamMaxHealth;
			enemyHealth = snapshot.ENEMY.teamHealth;
			enemyMaxHealth = snapshot.ENEMY.teamMaxHealth;

			snapshotDescription = description;

			Debug.Log("Snapped!" + this.ToString());
		}

		public override string ToString ()
		{
			return string.Format ("[UISnapshot: Player Health: {0}/{1}, Enemy Health: {2}/{3}]", 
				playerHealth, playerMaxHealth, enemyHealth, enemyMaxHealth);
		}
	}

}
