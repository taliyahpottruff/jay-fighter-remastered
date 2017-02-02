using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour {
    public GameObject storeObject;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("Press");
            bool current = storeObject.activeSelf;
            bool b = !current;
            storeObject.SetActive(b);
            Game.PAUSED = b;
        }
    }
}