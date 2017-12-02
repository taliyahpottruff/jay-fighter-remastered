using UnityEngine;

public class PlatingUpgrade : Item {
    public PlatingUpgrade() {
        SetName("Plating Upgrade");
        SetCost(100);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/PlatingUpgrade"));
    }

    public PlatingUpgrade(int amount) {
        AddItems(amount);
        SetName("Plating Upgrade");
        SetCost(100);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/PlatingUpgrade"));
    }

    protected override GameObject Action(Player player) {
        //Increase Player's Max Health by 10
        player.IncreaseMaxHealth(10);
        return null;
    }
}
