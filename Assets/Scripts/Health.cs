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
    private GameObject Coin;
    public void Start() {
        Coin = Resources.Load<GameObject>("Prefabs/Coin");
    }

    public float GetHealth() {
        return health;    
    }
    public float GetMaxHealth() {
        return maxHeath;
    }

    public void DoDamage(float attack) {

        if (health < attack) {
            //Entity dies
            CPU cpu = this.gameObject.GetComponent<CPU>();
            if (cpu != null) {
                Instantiate(Coin, this.gameObject.transform.position, Quaternion.identity);
            }
            GameManager.addScore(100);
            Destroy(this.gameObject);
        }
        else {
            health -= attack;
        }
    }
}
