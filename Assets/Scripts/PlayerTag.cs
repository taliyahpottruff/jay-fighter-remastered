using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: Taliyah Pottruff
/// </summary>

public class PlayerTag : MonoBehaviour {
    [SerializeField]
    GameObject playerTag;
    public new Text name;

    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        if (player.isLocalPlayer) playerTag.SetActive(false); //Don't show someone their own name

        name.text = player.username;
    }
}