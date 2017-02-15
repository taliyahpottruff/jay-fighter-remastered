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
    private GameObject BronzeCoin;
    private GameObject SilverCoin;
    private GameObject GoldCoin;
    public void Start() {
        BronzeCoin = Resources.Load<GameObject>("Prefabs/BronzeCoin");
        SilverCoin = Resources.Load<GameObject>("Prefabs/SilverCoin");
        GoldCoin = Resources.Load<GameObject>("Prefabs/GoldCoin");
    }

    public float GetHealth() {
        return health;    
    }
    public float GetMaxHealth() {
        return maxHeath;
    }

    public void DoDamage(float attack) {

        if (health <= attack) {
            //Entity dies
            CPU cpu = this.gameObject.GetComponent<CPU>();
            if (cpu != null) {
                if (cpu.shooter) {
                    Instantiate(GoldCoin, this.gameObject.transform.position, Quaternion.identity);
                }else if (cpu.duplicator) {
                    float cointype = Mathf.Round(Random.Range(0f, 4f));
                    if(cointype == 0f) {
                        Instantiate(GoldCoin, this.gameObject.transform.position, Quaternion.identity);
                    } else {
                        Instantiate(SilverCoin, this.gameObject.transform.position, Quaternion.identity);
                    }
                } else {
                    float cointype = Mathf.Round(Random.Range(0f, 4f));
                    if (cointype == 0f) {
                        Instantiate(SilverCoin, this.gameObject.transform.position, Quaternion.identity);
                    } else {
                        Instantiate(BronzeCoin, this.gameObject.transform.position, Quaternion.identity);
                    }
                }
                //Instantiate(Coin, this.gameObject.transform.position, Quaternion.identity);
                GameManager.addScore(cpu.ScoreOnDeath);
            }
            Destroy(this.gameObject);
        }
        else {
            health -= attack;
        }
    }
}
