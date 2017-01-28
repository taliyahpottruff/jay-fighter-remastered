using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour {
    public Inventory inventory;

    public void Start() {
        StartCoroutine(DelayedStart());
    }

    public void Update() {
        if (inventory.inventory.Count <= 0)
            this.transform.parent.gameObject.SetActive(false);
        else
            this.transform.parent.gameObject.SetActive(true);
    }

    private IEnumerator DelayedStart() {
        yield return new WaitForSeconds(0.1f);

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        Clear();
        AddAllItems();
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