using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Facepunch.Steamworks;

/*
 * AUTHOR: Trenton Pottruff
 * CONTRIBUTOR: Garrett Nicholas
 * (added the checks for the enemy death then spawns a coin)
 */

public class Health : MonoBehaviour {
    [SerializeField]
    private bool invincible;

    public float health = 100;
    private float maxHeath = 100;

    private GameObject explosionPrefab;

    private void Start() {
        explosionPrefab = Resources.Load<GameObject>("Prefabs/Death Explosion");
    }

    public void Update() {
        if(health > maxHeath) {
            health = maxHeath;
        }
    }
    /// <summary>
    /// Gets the current health
    /// </summary>
    /// <returns>The current health level of this entit.y</returns>
    public float GetHealth() {
        return health;    
    }

    /// <summary>
    /// Gets the max health
    /// </summary>
    /// <returns>The maximum health level of this entity.</returns>
    public float GetMaxHealth() {
        return maxHeath;
    }

    /// <summary>
    /// Increases the entity's max health.
    /// </summary>
    /// <param name="amount">The amount to increase max health by</param>
    public void IncreaseMax(int amount) {
        maxHeath += amount;
    }

    /// <summary>
    /// Does damage to this enemy.
    /// </summary>
    /// <param name="attack">The amount of damage to deal</param>
    /// <returns>True if the damage killed the entity and False if it didn't</returns>
    public bool DoDamage(float attack) {
        if (!invincible) {
            if (health <= attack) { //The attack will kill player
                                    //Entity dies
                CPU cpu = this.gameObject.GetComponent<CPU>();
                if (cpu != null) {
                    cpu.disposeTimer();
                    cpu.DropCoins();
                    GameManager.addScore(cpu.ScoreOnDeath);
                    if (Game.STEAM != null)
                    Game.STEAM.GiveAchievement("FIGHTER");
                }
                Instantiate<GameObject>(explosionPrefab, this.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                return true;
            }
            else {
                health -= attack;
                CPU cpu = this.gameObject.GetComponent<CPU>();
                if (cpu != null) {
                    cpu.resetTimer();
                }
                return false;
            }
        }

        return false;
    }
}
