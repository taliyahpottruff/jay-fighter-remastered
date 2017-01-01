using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/*
    * AUTHOR: Trenton Pottruff
*/
public class SettingsManager : MonoBehaviour {
    public Dropdown resolutionDropdown;
    public InputField resolutionWidth;
    public InputField resolutionHeight;

    private void Start() {
        GameObject.Find("Fullscreen Toggle").GetComponent<Toggle>().isOn = Screen.fullScreen;

        Resolution[] resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++) {
            resolutionDropdown.AddOptions(new List<string> { resolutions[i].width + "x" + resolutions[i].height });
        }

        resolutionWidth.text = Screen.currentResolution.width.ToString();
        resolutionHeight.text = Screen.currentResolution.height.ToString();
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

    public void SetResolutionBoth(string width, string height) {
        Screen.SetResolution(int.Parse(width), int.Parse(height), Screen.fullScreen);
    }

    public void SelectResolution(int option) {
        string resoString = resolutionDropdown.options[option].text;

        if (resoString.Equals("Custom")) {
            resolutionWidth.interactable = true;
            resolutionHeight.interactable = true;

            return;
        }

        resolutionWidth.interactable = false;
        resolutionHeight.interactable = false;

        string[] parts = resoString.Split('x');
        SetResolutionBoth(parts[0], parts[1]);
        resolutionWidth.text = parts[0];
        resolutionHeight.text = parts[1];
    }
}