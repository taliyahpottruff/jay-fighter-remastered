using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

public class Toolbar : MonoBehaviour {
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
            if (inventory.inventory.Count <= 0) {
                panelBody.enabled = false;
                panelStrip.enabled = false;
            }
            else {
                panelBody.enabled = true;
                panelStrip.enabled = true;
            }

            int currCount = inventory.inventory.Count;

            if (prevCount != currCount && hasStarted) {
                Clear();
                AddAllItems();
            }

            prevCount = currCount;
        } catch (Exception e) { /*Do nothing*/ }
    }

    private IEnumerator DelayedStart() {
        yield return new WaitForSeconds(0.11f);
        Debug.Log("Toolbar Delayed Start Executing...");
        
        inventory = NetworkManager.singleton.client.connection.playerControllers[0].gameObject.GetComponent<Inventory>();
        prevCount = inventory.inventory.Count;

        Clear();
        AddAllItems(); //Add all the items to store

        hasStarted = true;
    }

    public void Clear() {
        for (int i = 0; i < this.transform.childCount; i++) {
            Destroy(this.transform.GetChild(i));
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