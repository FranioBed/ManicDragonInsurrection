﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : BaseAttackBehaviour {

	Coroutine fightCoroutine;

	void Start () {
		myself.onStateChange.AddListener (HandleStateChange);
	}
	
	void HandleStateChange() {
		if (myself.state == Enemy.State.Fight) {
			fightCoroutine = StartCoroutine (Fight());
		} else if (fightCoroutine != null) {
			StopCoroutine (fightCoroutine);
		}
	}

	IEnumerator Fight() {
		while (true) {
			myself.player.RecieveDamage (GetRandomAttackPower ());
			yield return new WaitForSeconds (attacksTimeOffset);
		}
	}

	float GetRandomAttackPower() {
		return Random.Range(minAttackPower, maxAttackPower);
	}

}
