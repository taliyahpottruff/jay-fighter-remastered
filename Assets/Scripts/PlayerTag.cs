using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerTag : NetworkBehaviour {
    [SerializeField]
    GameObject playerTag;
    public new Text name;

    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        if (isLocalPlayer)
            playerTag.SetActive(false);

        name.text = player.username;
    }
}