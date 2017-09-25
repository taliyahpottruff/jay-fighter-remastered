using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("Press");
            bool current = storeObject.activeSelf;
            bool b = !current;
            storeObject.SetActive(b);
            Game.PAUSED = b;
        }
    }
    public void handleExit() {
        storeObject.SetActive(false);
        if (!pause.getOpened()) {
            GameUI.SetActive(true);
            Game.PAUSED = false;
        }
    }

    public void SetGamePaused(bool b) {
        Game.PAUSED = b;
    }
}
