using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item {
    private int amount;
    private string name;
    private Sprite sprite;
    
    public void Consume(Player player) {
        Action(player);

        amount--;
    }

    public void AddItems(int amount) {
        this.amount += amount;
    }

    public int GetAmount() {
        return amount;
    }

    public string GetName() {
        return name;
    }

    public void SetName(string name) {
        this.name = name;
    }

    public void SetSprite(Sprite sprite) {
        this.sprite = sprite;
    }

    public Sprite GetSprite() {
        return sprite;
    }

    protected abstract void Action(Player player);
}