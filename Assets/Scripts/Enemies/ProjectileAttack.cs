using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : BaseAttackBehaviour {
    public GameObject projectilePrefab;

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
            //myself.player.RecieveDamage (GetRandomAttackPower ());
            Vector2 target = myself.player.transform.position;
            Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
            Vector2 direction = target - myPos;
            direction.Normalize();
            GameObject projectile = (GameObject)Instantiate(projectilePrefab, myPos, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectile.GetComponent<Projectile>().speed;
            projectile.transform.LookAt(transform.position + new Vector3(0, 0, 1), direction);
            yield return new WaitForSeconds (attacksTimeOffset);
		}
	}

	float GetRandomAttackPower() {
		return Random.Range(minAttackPower, maxAttackPower);
	}
}
