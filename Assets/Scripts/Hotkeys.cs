using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public class Hotkeys : MonoBehaviour {
    private PauseScreenManager pauseScreenManager;
    public GameObject store;
    public SettingsManager settings;
    private GameObject gameUI;

    private void Start() {
        //Grab manager components
        pauseScreenManager = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<PauseScreenManager>();
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
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
