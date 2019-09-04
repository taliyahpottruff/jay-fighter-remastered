using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

public class PlayerTag : MonoBehaviour {
    [SerializeField]
    GameObject playerTag;
    public new Text name;

    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
		playerTag.SetActive(false); //Don't show someone their own name
	}

    private void Update() {
        name.text = player.username;
    }
}