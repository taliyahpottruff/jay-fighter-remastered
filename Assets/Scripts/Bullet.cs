using UnityEngine;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 * CONTRIBUTOR: Garrett Nicholas
 */

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {
    public int damage = 10;
    public bool playerBullet; //Is this bullet a playey bullet
    public GameObject owner; //Used to identify who is shooting the bullet
    private Rigidbody2D rb;
    private Vector2 velocity;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Game.PAUSED) { //Make sure the bullet doesn't move when the game is paused
            rb.velocity = Vector2.zero;
        } else {
            rb.velocity = velocity;
            velocity = rb.velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
            if ((other.gameObject.tag.Equals("Player") && !playerBullet) || (!other.gameObject.tag.Equals("Player") && playerBullet)) {
                Health health = other.GetComponent<Health>();

                Destroy(this.gameObject);
                //Only if the other object has a health component
                if (health != null) {
                    if (health.DoDamage(damage)) {
                        if (playerBullet) {
                            Player p = owner.GetComponent<Player>();
                            p.score += 1f;
                        }
                    }
                }
            }
    }

    /// <summary>
    /// Sets the velocity of the bullet.
    /// </summary>
    /// <param name="newVelocity">The new velocity</param>
    public void SetVelocity(Vector2 newVelocity) {
        rb.velocity = newVelocity;
    }
}
