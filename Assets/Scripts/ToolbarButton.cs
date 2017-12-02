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

    /// <summary>
    /// Executes the action on click of this button
    /// </summary>
    public void Click() {
        inv.Consume(text.text, this.gameObject);
    }
}