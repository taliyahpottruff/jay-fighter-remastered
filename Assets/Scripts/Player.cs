using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(Health))]
public class Player : NetworkBehaviour {
    public string username = "Player";
    public ControlScheme currentScheme = ControlScheme.Keyboard;

    private SpriteRenderer sr;
    private Health health;

    public override void OnStartLocalPlayer() {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.blue;

        //Set local camera to follow this player
        SmoothCamera sc = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothCamera>();
        if (sc != null) sc.lookAt = this.transform;
    }

    public void Start() {
        health = GetComponent<Health>();
    }

    private void Update() {
        if ((Input.anyKey && !Input.GetButton("Fire1")) || Input.GetMouseButton(0))
            currentScheme = ControlScheme.Keyboard;
        else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            currentScheme = ControlScheme.Gamepad;
    }

    public void GiveHealth(int amount) {
        health.health += amount;
    }
}
