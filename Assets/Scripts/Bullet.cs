using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : NetworkBehaviour {
    private Vector2 velocityOnAwake = Vector2.zero;
    public int damage = 10;
    private Rigidbody2D rb;

    private void Start() {
        if (!isLocalPlayer)
            return;

        rb = GetComponent<Rigidbody2D>();

        rb.velocity = velocityOnAwake;
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
