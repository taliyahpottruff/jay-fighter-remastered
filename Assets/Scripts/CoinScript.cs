using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: Garrett Nicholas
 * MODIFICATIONS: Trenton Pottruff
 */

public class CoinScript : MonoBehaviour {
    public int coinValue;

    private void OnTriggerEnter2D(Collider2D other) { //When a player touches the coin, add that coin value to the player and destroy the coin
        Player player = other.GetComponent<Player>();
        if (player != null) {
            player.coins += this.coinValue;
            Destroy(this.gameObject);
        }
    }
}
