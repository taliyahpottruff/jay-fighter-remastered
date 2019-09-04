using UnityEngine;

// AUTHOR: Trenton Pottruff
public class MineItem : Item {
    public MineItem() {
        SetName("Mine");
        SetCost(10);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/Mine"));
    }

	/// <summary>
	/// Sets up a Mine item.
	/// </summary>
	/// <param name="amount">The number of mines</param>
    public MineItem(int amount) {
        AddItems(amount);
        SetName("Mine");
        SetCost(10);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/Mine"));
    }

    protected override GameObject Action(Player player) {
        return Resources.Load<GameObject>("Prefabs/Mine");
    }
}