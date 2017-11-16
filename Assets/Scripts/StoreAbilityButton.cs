using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: Trenton Pottruff
/// </summary>
public class StoreAbilityButton : MonoBehaviour {
    public string abilityName;
    public int cost;

    private Text button;
    private Player player;
    private PlayerAbilities playerAbilities;
    private Button itemButton;

    private void Start() {
        StartCoroutine(DelayedStart());
        itemButton = GetComponent<Button>();
    }

    private IEnumerator DelayedStart() {
        yield return new WaitForSeconds(0.11f);
        button = GetComponentInChildren<Text>();
        GameObject playerObj = NetworkManager.singleton.client.connection.playerControllers[0].gameObject;
        player = playerObj.GetComponent<Player>();
        playerAbilities = playerObj.GetComponent<PlayerAbilities>();
    }

    private void Update() {
        //Check to see if the player can afford the item
        bool itemAvailable = false;
        if (player != null) {
            if (player.coins >= cost)
                itemAvailable = true;
        }

        itemButton.interactable = itemAvailable;

        if (button == null) {
            button = GetComponentInChildren<Text>();
        }

        button.text = abilityName + "\n($" + cost + ")";
    }

    /// <summary>
    /// Buys the item
    /// </summary>
    public void BuyItem() {
        if (player.coins < cost) return;
        player.coins -= cost;

        playerAbilities.AddAbility(abilityName);
    }
}