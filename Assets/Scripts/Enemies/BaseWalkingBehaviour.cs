using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWalkingBehaviour : MonoBehaviour {
	public float walkingSpeed;
	protected Enemy myself;
	protected Rigidbody2D rb2d;

	public void Awake() {
		myself = GetComponent<Enemy> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
}
