using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : BaseAttackBehaviour {

	bool fightStarted = false;

	void Start () {
		myself.onStateChange.AddListener (HandleStateChange);
	}
	
	void HandleStateChange() {
		if (myself.state == Enemy.State.Fight && !fightStarted) {
			StartCoroutine (Fight());
			fightStarted = true;
		} else if (fightStarted) {
			StopAllCoroutines ();
			fightStarted = false;
		}
	}

	IEnumerator Fight() {
		for (;;) {
			yield return new WaitForSeconds (attacksTimeOffset);
			myself.player.RecieveDamage (GetRandomAttackPower ());
		}
	}

	float GetRandomAttackPower() {
		return Random.Range(minAttackPower, maxAttackPower);
	}

}
