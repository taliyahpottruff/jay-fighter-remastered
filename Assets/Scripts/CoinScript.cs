using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    * AUTHOR: Garrett Nicholas
    * MODIFICATIONS: Trenton Pottruff
*/

public class CoinScript : MonoBehaviour {
    public int coinValue;

    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();
        if (player != null) {
            player.coins += this.coinValue;
            Destroy(this.gameObject);
        }
    }
}
