using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(Health))]
public class Player : NetworkBehaviour {
    public string username = "Player";

    private SpriteRenderer sr;
    private Health health;

    public override void OnStartLocalPlayer() {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.blue;
    }

    public void Start() {
        health = GetComponent<Health>();
    }

    public void GiveHealth(int amount) {
        health.health += amount;
    }
}
