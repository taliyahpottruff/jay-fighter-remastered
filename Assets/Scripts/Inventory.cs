using System.Collections.Generic;
using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public class Inventory : MonoBehaviour {
    public List<Item> inventory = new List<Item>();
    public int max = 0; //A maximum item limit, if any.
    //TODO Implement max inventory limits

    private GameObject toSpawn;

    public void Consume(string name, GameObject toDelete) {
        Debug.Log("Consuming...");

        int index = -1;
        //Search for item
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].GetName().Equals(name)) {
                index = i;
                break;
            }
        }

        //Check to see if the item was found
        if (index < 0) {
            Debug.LogError("The item attempting to be consumed wasn't found.");
            return;
        }

        toSpawn = inventory[index].Consume(this.gameObject.GetComponent<Player>());

        if (toSpawn != null) {
            Debug.Log("Spawning...");
            CmdSpawn(toSpawn.name);
        } else {
            Debug.Log("Nope");
        }

        if (inventory[index].GetAmount() <= 0) {
            inventory.RemoveAt(index);
            Destroy(toDelete);
        }
    }

    private void CmdSpawn(string name) {
        Debug.Log("Doit.");
        GameObject go = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/" + name), this.transform.position, Quaternion.identity);
        //NetworkServer.Spawn(go);
    }
}