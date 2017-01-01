using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/
public class SettingsManager : MonoBehaviour {
    private void Start() {
        GameObject.Find("Fullscreen Toggle").GetComponent<Toggle>().isOn = Screen.fullScreen;
    }

    public void SetFullscreen(bool fullscreen) {
        Screen.fullScreen = fullscreen;
    }

    public void SetResolutionWidth(string size) {
        Screen.SetResolution(int.Parse(size), Screen.height, Screen.fullScreen);
    }

    public void SetResolutionHeight(string size) {
        Screen.SetResolution(Screen.width, int.Parse(size), Screen.fullScreen);
    }
}