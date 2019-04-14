using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 * CONTRIBUTOR: Garrett Nicholas
 */

[System.Obsolete("Implements a class that uses old Unity networking")]
public class Store : MonoBehaviour {
    public GameObject storeObject;
    private PauseScreenManager pause;
    private GameObject GameUI;

    private void Start() {
        pause = GameObject.FindGameObjectWithTag("PauseScreen").GetComponent<PauseScreenManager>();
        GameUI = GameObject.FindGameObjectWithTag("GameUI");
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            bool current = storeObject.activeSelf; //Is the store open rn?
            bool b = !current; //Toggle the store
            storeObject.SetActive(b);
            Game.PAUSED = b;
        }
    }

    /// <summary>
    /// Handles exiting the store
    /// </summary>
    public void handleExit() {
        storeObject.SetActive(false);
        if (!pause.getOpened()) {
            GameUI.SetActive(true);
            Game.PAUSED = false;
        }
    }

    /// <summary>
    /// Sets the pause state
    /// </summary>
    /// <param name="b">Whether the game is pause or not</param>
    public void SetGamePaused(bool b) {
        Game.PAUSED = b;
    }
}