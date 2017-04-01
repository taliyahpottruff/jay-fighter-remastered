using UnityEngine;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
    * CONTRIBUTOR: Garrett Nicholas
    * (added the checks for the enemy death then spawns a coin)
*/

public class Health : MonoBehaviour {
    public float health = 100;
    private float maxHeath = 100;
   
    public void Update() {
        if(health > maxHeath) {
            health = maxHeath;
        }
    }

    public float GetHealth() {
        return health;    
    }
    public float GetMaxHealth() {
        return maxHeath;
    }

    public void DoDamage(float attack) {

        if (health <= attack) { //The attack will kill player
            //Entity dies
            CPU cpu = this.gameObject.GetComponent<CPU>();
            if (cpu != null) {
                cpu.disposeTimer();
                cpu.DropCoins();
                GameManager.addScore(cpu.ScoreOnDeath);
            }
            Destroy(this.gameObject);
        }
        else {
            health -= attack;
            CPU cpu = this.gameObject.GetComponent<CPU>();
            if (cpu != null) {
                cpu.resetTimer();
            }
        }
    }
}
