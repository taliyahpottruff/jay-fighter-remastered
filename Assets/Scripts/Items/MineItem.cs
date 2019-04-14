using UnityEngine;

public class MineItem : Item {
    public MineItem() {
        SetName("Mine");
        SetCost(10);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/Mine"));
    }

    public MineItem(int amount) {
        AddItems(amount);
        SetName("Mine");
        SetCost(10);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/Mine"));
    }

    protected override GameObject Action(Player player) {
        //player.CmdSpawnItem("Mine");
        return Resources.Load<GameObject>("Prefabs/Mine");
    }
}