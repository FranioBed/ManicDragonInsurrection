using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float range = 10f;
    public float speed = 10f;
    public float damage = 5f;

    private float distanceTravelled = 0f;
    private Vector2 startPos;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        distanceTravelled = Vector2.Distance(transform.position, startPos);
        if (distanceTravelled > range)
        {
            Destroy(gameObject);
        }
	}

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            coll.gameObject.GetComponent<Enemy>().RecieveDamage(damage);
        }
        if (coll.gameObject.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<Player>().RecieveDamage(damage);
        }
        Destroy(gameObject);
    }
}
