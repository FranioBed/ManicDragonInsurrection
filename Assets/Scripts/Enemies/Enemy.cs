using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour {
	public float startHealth;
	public float seesPlayesDistance;
	public float attackDistance;

	public UnityEvent onStateChange;

    private float stunTimer = 0;
	public State state {
		get {
			return _state;
		}
		set {
			if (_state != value) {
				_state = value;
				UpdateAnimator ();
				onStateChange.Invoke ();
			}
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
        } else if (state == State.Stunned) {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0)
            {
                state = State.Idle;
            }
        } else if (state == State.Dead) {
			Destroy (gameObject);
		}
	}

	public void RecieveDamage(float damage) {
		health = health - damage;
		if (health < 0) {
			state = State.Dead;
		}
	}

    public void RecieveKnockback(float stunDuration, Vector2 knockback)
    {
        state = State.Stunned;
        stunTimer = stunDuration;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
    }

	float CalculateDistanceToPlayer() {
		return (player.transform.position - transform.position).magnitude;
		
	}

	void UpdateAnimator() {
        //TODO: stun animation
		if (state == State.Idle || state == State.Stunned) {
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
        Stunned,
		Dead	
	}
}
