using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimeCheck : MonoBehaviour {
    [SerializeField]
    MenuManager menuManager;

    private void Start() {
        if (!PlayerPrefs.HasKey("username"))
            menuManager.ChangeMenu(4);
    }
}