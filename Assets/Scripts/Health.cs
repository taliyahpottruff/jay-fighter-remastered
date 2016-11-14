using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    private float health = 100;
    private float maxHeath = 100;
    public void Start() {}

    public float GetHealth() {
        return health;    
    }

    public void DoDamage(float attack) {
        if (health < attack) {
            //Entity dies
            GameManager.addScore(100);
            Destroy(this.gameObject);
        }
        else {
            health -= attack;
        }
    }
}
