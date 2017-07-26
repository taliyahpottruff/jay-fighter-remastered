using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Explosion : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        Health health = collision.gameObject.GetComponent<Health>();
        
        if (health != null) {
            health.DoDamage(50);
        }
    }

    public void EndExplosion() {
        Destroy(this.gameObject);
    }
}