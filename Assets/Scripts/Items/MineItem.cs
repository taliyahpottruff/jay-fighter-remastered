using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineItem : Item {
    public MineItem() {
        AddItems(1);
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

    protected override void Action(Player player) {
        player.CmdSpawnItem("Mine");
    }
}