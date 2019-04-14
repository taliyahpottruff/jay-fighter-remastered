using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Collider2D))]
[System.Obsolete("Implements a class that uses old Unity networking")]
public class Explosion : MonoBehaviour {
    [SerializeField]
    private float explosionDamage;

    private void OnTriggerEnter2D(Collider2D collision) {
		//If there is an object with health in the range of this explosion, do damage to it.
        Health health = collision.gameObject.GetComponent<Health>();
        
        if (health != null) {
            health.DoDamage(explosionDamage);
        }
    }

    public void EndExplosion() {
        Destroy(this.gameObject); //Destroy this object once the explosion is done
    }
}