using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWalkTowardsPlayer : BaseWalkingBehaviour {

	void FixedUpdate () {
		if (myself.state == Enemy.State.WalkToPlayer) {
			Vector2 direction = (myself.player.transform.position - myself.transform.position).normalized;
			rb2d.velocity = direction.normalized * walkingSpeed;
		}
	}
}
