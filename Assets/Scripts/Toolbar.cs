using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        //Debug.Log("Inventory contains " + inventory.inventory.Count + " items!");

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
    }

    private IEnumerator DelayedStart() {
        yield return new WaitForSeconds(0.1f);

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        prevCount = inventory.inventory.Count;

        Clear();
        AddAllItems();

        hasStarted = true;
    }

    public void Clear() {
        for (int i = 0; i < this.transform.childCount; i++) {
            Destroy(this.transform.GetChild(i));
        }
    }

    public void AddAllItems() {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Toolbar Button");

        for (int i = 0; i < inventory.inventory.Count; i++) {
            GameObject go = Instantiate(prefab, this.transform);
            go.name = prefab.name;
            go.GetComponent<ToolbarButton>().Init(inventory, i);
        }
    }
}