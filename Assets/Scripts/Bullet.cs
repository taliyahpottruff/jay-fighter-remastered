using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {
    private Vector2 velocityOnAwake = Vector2.zero;

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = velocityOnAwake;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Health health = other.GetComponent<Health>();
        Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();

        Destroy(this.gameObject);
        //Only if the other object has a health component
        if (health != null) {
            health.DoDamage(10);
        }

    }

    public void SetVelocity(Vector2 newVelocity) {
        rb.velocity = newVelocity;
    }

    public void SetVelocityOnAwake(Vector2 velocity) {
        velocityOnAwake = velocity;
    }
}
