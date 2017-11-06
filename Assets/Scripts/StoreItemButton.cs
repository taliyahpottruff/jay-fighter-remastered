using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

public class StoreItemButton : MonoBehaviour {
    public string itemName;
    public int cost;

    public Image itemIcon;

    private Text button;
    private Player player;
    private Inventory playerInventory;
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
        playerInventory = playerObj.GetComponent<Inventory>();
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

        button.text = itemName + "\n($" + cost + ")";
        itemIcon.sprite = Game.ITEMS[itemName].GetSprite();
    }

    /// <summary>
    /// Buys the item
    /// </summary>
    public void BuyItem() {
        if (player.coins < cost) return;
        player.coins -= cost;

        //Loop through inventory to see if item already exists
        for (int i = 0; i < playerInventory.inventory.Count; i++) {
            if (playerInventory.inventory[i].GetName().Equals(itemName)) {
                //Add the item if it already exists
                playerInventory.inventory[i].AddItems(1);
                return;
            }
        }

        //If not, create a new slot
        playerInventory.inventory.Add(Game.ITEMS[itemName]);
    }
}
