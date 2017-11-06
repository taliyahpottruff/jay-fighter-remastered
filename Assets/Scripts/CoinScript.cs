using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: Garrett Nicholas
 * MODIFICATIONS: Trenton Pottruff
 */

[RequireComponent(typeof(SpriteRenderer))]
public class CoinScript : MonoBehaviour {
    public int coinValue;

    private SpriteRenderer sr;

    float lifespan = 100f;
    float flashTime;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(DissapearAfterTime());
    }

    private void OnTriggerEnter2D(Collider2D other) { //When a player touches the coin, add that coin value to the player and destroy the coin
        Player player = other.GetComponent<Player>();
        if (player != null) {
            player.coins += this.coinValue;
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Flashes the coin after it has lived 90% of it's lifespan and then destroys it at 100%.
    /// </summary>
    /// <returns>An IEnumarator for the co-routine</returns>
    private IEnumerator DissapearAfterTime() {
        float timeTillFlash = lifespan * 0.75f;
        float timeTillDeath = lifespan * 0.25f;

        yield return new WaitForSeconds(timeTillFlash);
        flashTime = Time.time;
        StartCoroutine(Flash(timeTillDeath));
        yield return new WaitForSeconds(timeTillDeath);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Flashes the coin.
    /// </summary>
    /// <returns>An IEnumarator for the co-routine</returns>
    private IEnumerator Flash(float timeTillDeath) {
        while (true) {
            float deltaFlash = Time.time - flashTime;
            float flashRate = 0.5f - (0.5f * (deltaFlash / timeTillDeath));

            sr.enabled = false;
            yield return new WaitForSeconds(flashRate);
            sr.enabled = true;
            yield return new WaitForSeconds(flashRate);
        }
    }
}
