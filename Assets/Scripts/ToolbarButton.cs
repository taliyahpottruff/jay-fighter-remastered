using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarButton : MonoBehaviour {
    public Image icon;
    public Text text;
    public Text amountText;
    public Inventory inv;
    public int index;

    public void Init(Inventory inventory, int index) {
        inv = inventory;
        this.index = index;
        text.text = inventory.inventory[index].GetName();
    }

    private void Update() {
        amountText.text = "(" + inv.inventory[index].GetAmount() + ")";
        icon.sprite = inv.inventory[index].GetSprite();
    }

    public void Click() {
        inv.inventory[index].Consume(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>());

        if (inv.inventory[index].GetAmount() <= 0) {
            inv.inventory.RemoveAt(index);
            Destroy(this.gameObject);
        }
    }
}