using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 1f;

    private Rigidbody2D rb2D;
    
	void Start () {
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Debug.Log("H: " + horizontal + ", V: " + vertical);
        Vector2 direction = new Vector2(horizontal, vertical);
        rb2D.velocity = (direction.magnitude > 1) ? direction.normalized * speed : direction * speed;
        //rb2D.AddForce(direction * speed);
    }
}
