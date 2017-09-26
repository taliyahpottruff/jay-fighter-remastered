using UnityEngine;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : NetworkBehaviour {
    public int damage = 10;
    public bool playerBullet; //Is this bullet a playey bullet
    public NetworkIdentity owner; //Used to identify who is shooting the bullet
    private Rigidbody2D rb;
    private Vector2 velocity;

    private void Start() {
        if (!isLocalPlayer) return; //If this bullet doesn't belong to the player rendering it, then ignore the rest

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!isLocalPlayer) return;

        if (Game.PAUSED) { //Make sure the bullet doesn't move when the game is paused
            rb.velocity = Vector2.zero;
        } else {
            rb.velocity = velocity;
            velocity = rb.velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isServer) { //Only handle hit events on the server
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
    }

    /// <summary>
    /// Sets the velocity of the bullet.
    /// </summary>
    /// <param name="newVelocity">The new velocity</param>
    public void SetVelocity(Vector2 newVelocity) {
        rb.velocity = newVelocity;
    }
}
