using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

public class Player : NetworkBehaviour {
    public string username = "Player";

    private SpriteRenderer sr;

    public override void OnStartLocalPlayer() {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.blue;
    }
}
