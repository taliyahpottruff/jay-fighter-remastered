using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[System.Obsolete("Implements a class that uses old Unity networking")]
public class ItemList : MonoBehaviour {
    public GameObject buttonPrefab;

    private void Start() {
        Item[] items = new Item[Game.ITEMS.Count];
        Game.ITEMS.Values.CopyTo(items, 0);

        //Populate the store page with items
        for(int i = 0; i < items.Length; i++) {
            Debug.Log(items[i].GetName());
            GameObject go = Instantiate(buttonPrefab, this.transform);
            StoreItemButton button = go.GetComponent<StoreItemButton>();
            button.itemName = items[i].GetName();
            button.cost = items[i].GetCost();
        }
    }
}