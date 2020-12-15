using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: Taliyah Pottruff
/// </summary>
[System.Obsolete("Uses Unity's old networking features")]
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
		//Wait a fraction of a second before initializing
        yield return new WaitForSeconds(0.11f);
        button = GetComponentInChildren<Text>();
        GameObject playerObj = Player.singleton.gameObject;
        player = Player.singleton;
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