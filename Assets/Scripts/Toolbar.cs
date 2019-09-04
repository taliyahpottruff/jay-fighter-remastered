using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

[System.Obsolete("Uses Unity's old networking features")]
public class Toolbar : NetworkBehaviour {
    public Image panelBody;
    public Image panelStrip;
    public Inventory inventory;

    private int prevCount = 0;
    private bool hasStarted = false;

    public void Start() {
        
        StartCoroutine(DelayedStart());
    }

    public void Update() {
        try {
            if (inventory == null) return;

            if (inventory.inventory.Count <= 0) {
                panelBody.enabled = false;
                panelStrip.enabled = false;
            }
            else {
                panelBody.enabled = true;
                panelStrip.enabled = true;
            }

            if (hasStarted) {
                int currCount = GetInventoryCount();

                if (prevCount != currCount) {
                    Clear();
                    AddAllItems();
                }

                prevCount = currCount;
            }
        } catch (Exception e) {
            Debug.LogError(e.StackTrace);
        }
    }

    private int GetInventoryCount() {
        int c = 0;

        for (int i = 0; i < inventory.inventory.Count; i++) {
            c += inventory.inventory[i].GetAmount();
        }

        return c;
    }

    private IEnumerator DelayedStart() {
        yield return new WaitForSeconds(0.11f);
        Debug.Log("Toolbar Delayed Start Executing...");
        
        prevCount = 0;

        Clear();

        hasStarted = true;
    }

    public void Clear() {
        for (int i = 0; i < this.transform.childCount; i++) {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Adds all available items to the store for purchase
    /// </summary>
    public void AddAllItems() {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Toolbar Button");

        for (int i = 0; i < inventory.inventory.Count; i++) {
            GameObject go = Instantiate(prefab, this.transform);
            go.name = prefab.name;
            go.GetComponent<ToolbarButton>().Init(inventory, i);
        }
    }
}