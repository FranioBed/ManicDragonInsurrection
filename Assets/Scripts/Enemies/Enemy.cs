using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public float startHealth;
	public float seesPlayesDistance;
	public float attackDistance;

	public float health { get; private set;}
	public State state;

	private AnimationController myAnimationController;
	private Rigidbody2D rb2d;
	private Player player;
	private BaseWalkingBehaviour walkingBehaviour;
	private BaseAttackBehaviour attackBehaviour;


	void Awake() {
		rb2d = GetComponent<Rigidbody2D> ();
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

	public enum State {
		Idle,
		WalkToPlayer,
		Fight,
		Dead	
	}
}
