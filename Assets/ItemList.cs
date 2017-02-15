using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour {
    public GameObject buttonPrefab;

    private void Start() {
        Item[] items = new Item[Game.ITEMS.Count];
        Game.ITEMS.Values.CopyTo(items, 0);
        Debug.Log(items.Length + " items");
        for(int i = 0; i < items.Length; i++) {
            Debug.Log(items[i].GetName());
            GameObject go = Instantiate(buttonPrefab, this.transform);
            StoreItemButton button = go.GetComponent<StoreItemButton>();
            button.itemName = items[i].GetName();
            button.cost = items[i].GetCost();
        }
    }
}