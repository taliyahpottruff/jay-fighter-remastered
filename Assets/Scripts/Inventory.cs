using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Inventory : NetworkBehaviour {
    public List<Item> inventory = new List<Item>();
    [SyncVar]
    public int max = 0;
}