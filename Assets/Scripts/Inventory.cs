using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Item> inventory = new List<Item>();
    public int max = 0;

    public void Start() {
        inventory.Add(Game.ITEMS["healthPotion"]);
    }
}