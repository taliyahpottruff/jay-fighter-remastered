using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 * CONTRIBUTOR: Garrett Nicholas
 */

public class Hotkeys : MonoBehaviour {
    private PauseScreenManager pauseScreenManager;
    public GameObject store;
    public SettingsManager settings;

    private void Start() {
        //Grab manager components
        pauseScreenManager = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<PauseScreenManager>();
    }

    private void Update() {
        //Pause Screen Hotkey
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (store.activeSelf) {
                store.SetActive(false);
                if (!pauseScreenManager.getOpened()) {
                    Game.PAUSED = false;
                }
            } else if (settings.panel.activeSelf) {
                settings.CloseScreen();
                pauseScreenManager.OpenScreen();
            } else {
                pauseScreenManager.ToggleScreen();
            }
        }
    }
}
