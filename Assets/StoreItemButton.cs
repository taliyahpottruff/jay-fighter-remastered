using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class StoreItemButton : MonoBehaviour {
    public string itemName;
    public int cost;

    private Text button;
    private Player player;
    private Inventory playerInventory;

    private void Start() {
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart() {
        yield return new WaitForSeconds(0.11f);
        button = GetComponentInChildren<Text>();
        GameObject playerObj = NetworkManager.singleton.client.connection.playerControllers[0].gameObject;
        player = playerObj.GetComponent<Player>();
        playerInventory = playerObj.GetComponent<Inventory>();
    }

    private void Update() {
        button.text = itemName + "\n($" + cost + ")";
    }

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
