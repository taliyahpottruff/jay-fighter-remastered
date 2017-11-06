using UnityEngine;

public class HealthPotion : Item {
    public HealthPotion() {
        AddItems(1);
        SetName("Repair Kit");
        SetCost(30);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/RepairKit"));
    }

    public HealthPotion(int amount) {
        AddItems(amount);
        SetName("Repair Kit");
        SetCost(30);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/RepairKit"));
    }

    protected override void Action(Player player) {
        //Give player health
        player.GiveHealth(10);
    }
}