using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public float startHealth;
	public float seesPlayesDistance;
	public float attackDistance;
	public State state {
		get {
			return _state;
		}
		set {
			_state = value;
			UpdateAnimator ();
		}
	} 
	private State _state;

	public float health { get; private set;}
	public Player player { get; private set; }

	private AnimationController myAnimationController;

	void Awake() {
		myAnimationController = GetComponent<AnimationController> ();
		health = startHealth;	
		state = State.Idle;
	}

	void Start () {
		player = FindObjectOfType<Player> ();
	}
	
	void Update () {
		float distanceToPlayer = CalculateDistanceToPlayer ();
		if (state == State.Idle) {
			if (distanceToPlayer < seesPlayesDistance) {
				state = State.WalkToPlayer;
			}
		} else if (state == State.WalkToPlayer) {
			if (distanceToPlayer < attackDistance) {
				state = State.Fight;
			}
		} else if (state == State.Fight) {
			if (distanceToPlayer > attackDistance) {
				state = State.WalkToPlayer;
			}
		} else if (state == State.Dead) {
			Destroy (gameObject);
		}
	}

	public void GetDamage(float damage) {
		health = health - damage;
		if (health < 0) {
			state = State.Dead;
		}
	}

	float CalculateDistanceToPlayer() {
		return (player.transform.position - transform.position).magnitude;
		
	}

	void UpdateAnimator() {
		if (state == State.Idle) {
			myAnimationController.SetAnimationState (AnimationState.IDLE);
		} else if (state == State.WalkToPlayer) {
			myAnimationController.SetAnimationState (AnimationState.WALK);
		} else if (state == State.Fight) {
			myAnimationController.SetAnimationState (AnimationState.FIGHT);
		} else if (state == State.Dead) {
			myAnimationController.SetAnimationState (AnimationState.DIE);
		}
	}

	public enum State {
		Idle,
		WalkToPlayer,
		Fight,
		Dead	
	}
}
