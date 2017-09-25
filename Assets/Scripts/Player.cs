using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(Health))]
public class Player : NetworkBehaviour {
    [SyncVar] public string username = "Player";
    public ControlScheme currentScheme = ControlScheme.Keyboard;
    [SyncVar] public float score;
    [SyncVar] public int coins;

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

        if (isLocalPlayer) username = Game.STEAM.GetUsername();
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

    public void IncreaseMaxHealth(int amount) {
        health.IncreaseMax(amount);
    }

    [Command]
    public void CmdSpawnItem(string name) {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + name);
        GameObject go = Instantiate<GameObject>(prefab, this.transform.position, Quaternion.identity);
        NetworkServer.Spawn(go);
    }
}
