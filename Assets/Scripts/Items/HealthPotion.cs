using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item {
    public HealthPotion() {
        AddItems(1);
        SetName("Health Potion");
        SetCost(3);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/HealthPotion"));
    }

    public HealthPotion(int amount) {
        AddItems(amount);
        SetName("Health Potion");
        SetCost(3);
        SetSprite(Resources.Load<Sprite>("Sprites/Items/HealthPotion"));
    }

    protected override void Action(Player player) {
        //Give player health
        player.GiveHealth(10);
    }
}