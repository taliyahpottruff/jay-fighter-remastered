using UnityEngine;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

public class Hotkeys : MonoBehaviour {
    private PauseScreenManager pauseScreenManager;

    private void Start() {
        //Grab manager components
        pauseScreenManager = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<PauseScreenManager>();
    }

    private void Update() {
        //Pause Screen Hotkey
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseScreenManager.ToggleScreen();
        }
    }
}