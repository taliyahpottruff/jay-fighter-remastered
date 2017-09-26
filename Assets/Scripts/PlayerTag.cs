using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

public class PlayerTag : NetworkBehaviour {
    [SerializeField]
    GameObject playerTag;
    public new Text name;

    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        if (isLocalPlayer) playerTag.SetActive(false); //Don't show someone their own name

        name.text = player.username;
    }
}