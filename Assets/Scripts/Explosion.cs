using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Collider2D))]
public class Explosion : MonoBehaviour {
    [SerializeField]
    private float explosionDamage;

    private void OnTriggerEnter2D(Collider2D collision) {
        Health health = collision.gameObject.GetComponent<Health>();
        
        if (health != null) {
            health.DoDamage(explosionDamage);
        }
    }

    public void EndExplosion() {
        Destroy(this.gameObject);
    }
}