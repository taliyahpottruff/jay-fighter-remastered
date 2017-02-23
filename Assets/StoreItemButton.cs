using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemButton : MonoBehaviour {
    public string itemName;
    public int cost;

    private Text button;
    private Inventory playerInventory;

    private void Start() {
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart() {
        yield return new WaitForSeconds(0.11f);
        button = GetComponentInChildren<Text>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update() {
        button.text = itemName + "\n($" + cost + ")";
    }

    public void BuyItem() {
        if (GameManager.Coins < cost) return;
        GameManager.Coins -= cost;

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
