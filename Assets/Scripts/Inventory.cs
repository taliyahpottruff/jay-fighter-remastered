using System.Collections.Generic;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

public class Inventory : NetworkBehaviour {
    public List<Item> inventory = new List<Item>();
    [SyncVar]
    public int max = 0; //A maximum item limit, if any.
    //TODO Implement max inventory limits
}