using UnityEngine;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {
    private Vector2 velocityOnAwake = Vector2.zero;
    public int damage = 10;
    private Rigidbody2D rb;
    private Vector2 velocity;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        velocity = velocityOnAwake;
    }

    private void Update() {
        if (Game.PAUSED) {
            rb.velocity = Vector2.zero;
        } else {
            rb.velocity = velocity;
            velocity = rb.velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Health health = other.GetComponent<Health>();

        Destroy(this.gameObject);
        //Only if the other object has a health component
        if (health != null) {
            health.DoDamage(damage);
        }

    }

    public void SetVelocity(Vector2 newVelocity) {
        rb.velocity = newVelocity;
    }

    public void SetVelocityOnAwake(Vector2 velocity) {
        velocityOnAwake = velocity;
    }
}
