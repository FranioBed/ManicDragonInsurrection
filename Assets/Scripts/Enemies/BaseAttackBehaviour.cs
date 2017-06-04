using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttackBehaviour : MonoBehaviour {
	public float attacksTimeOffset;
	public float maxAttackPower;
	public float minAttackPower;
	protected Enemy myself;

	public void Awake() {
		myself = GetComponent<Enemy> ();
	}
}
