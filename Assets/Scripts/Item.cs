using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public abstract class Item {
    private int amount;
    private string name;
    private int cost;
    private Sprite sprite;
    
    /// <summary>
    /// Consumes one of this item
    /// </summary>
    /// <param name="player">The player who is consuming this item; in case it's relavent</param>
    public GameObject Consume(Player player) {
        amount--;
        return Action(player); //Executes the action of this item
    }

    /// <summary>
    /// Adds a specified amount of items to this stack
    /// </summary>
    /// <param name="amount">The amount of items to add</param>
    public void AddItems(int amount) {
        this.amount += amount;
    }
    
    /// <summary>
    /// Gets the amount of items in this stack
    /// </summary>
    /// <returns>The amount of items</returns>
    public int GetAmount() {
        return amount;
    }

    /// <summary>
    /// Gets the name of this item
    /// </summary>
    /// <returns>The name of this item</returns>
    public string GetName() {
        return name;
    }

    /// <summary>
    /// Sets the name of this item
    /// </summary>
    /// <param name="name">The new name for this item</param>
    public void SetName(string name) {
        this.name = name;
    }


    /// <summary>
    /// Sets the sprite for this item
    /// </summary>
    /// <param name="sprite">The new sprite for this item</param>
    public void SetSprite(Sprite sprite) {
        this.sprite = sprite;
    }

    /// <summary>
    /// Gets the sprite for this item
    /// </summary>
    /// <returns>The sprite of this item</returns>
    public Sprite GetSprite() {
        return sprite;
    }

    /// <summary>
    /// Gets the cost of this item
    /// </summary>
    /// <returns>The cost of this item</returns>
    public int GetCost() {
        return cost;
    }

    /// <summary>
    /// Sets the cost of this item
    /// </summary>
    /// <param name="cost">The new cost for this item</param>
    public void SetCost(int cost) {
        this.cost = cost;
    }

    /// <summary>
    /// An abstract method for executing whatever action the specified item needs to execute
    /// </summary>
    /// <param name="player">The player who is executing the action</param>
    protected abstract GameObject Action(Player player);
}