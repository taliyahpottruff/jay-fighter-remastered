using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    * AUTHOR: Garrett Nicholas
*/

public class CoinScript : MonoBehaviour {

    public int coinValue;
    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();
        if (player != null) {
            GameManager.Coins += this.coinValue;
            Destroy(this.gameObject);
        }
    }
}
