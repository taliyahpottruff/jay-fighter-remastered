using UnityEngine;

//AUTHOR: Trenton Pottruff
public class PlatingUpgrade : Item {
    public PlatingUpgrade() {
        SetName("Plating Upgrade");
        SetCost(100);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/PlatingUpgrade"));
    }

	/// <summary>
	/// Sets up a Plating Upgrade.
	/// </summary>
	/// <param name="amount">The number of items.</param>
    public PlatingUpgrade(int amount) {
        AddItems(amount);
        SetName("Plating Upgrade");
        SetCost(100);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/PlatingUpgrade"));
    }

    protected override GameObject Action(Player player) {
        //Increase Player's Max Health by 10
        player.IncreaseMaxHealth(10);
        float maxHealth = player.GetComponent<Health>().GetMaxHealth();
        player.GetComponent<Health>().health = maxHealth;
        return null;
    }
}
