using UnityEngine;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

public class Hotkeys : MonoBehaviour {
    private PauseScreenManager pauseScreenManager;
    public GameObject Store;
    private GameObject GameUI;

    private void Start() {
        //Grab manager components
        pauseScreenManager = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<PauseScreenManager>();
        GameUI = GameObject.FindGameObjectWithTag("GameUI");
    }

    private void Update() {
        //Pause Screen Hotkey
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Store.activeSelf) {
                Store.SetActive(false);
                if (!pauseScreenManager.getOpened()) {
                    GameUI.SetActive(true);
                    Game.PAUSED = false;
                }
            } else {
                pauseScreenManager.ToggleScreen();
                if (pauseScreenManager.getOpened()) {
                    GameUI.SetActive(false);
                } else {
                    GameUI.SetActive(true);
                }
            }
        }
    }
}
