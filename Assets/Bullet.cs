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
        Destroy(this.gameObject);
    }

    public void SetVelocity(Vector2 newVelocity) {
        rb.velocity = newVelocity;
    }

    public void SetVelocityOnAwake(Vector2 velocity) {
        velocityOnAwake = velocity;
    }
}