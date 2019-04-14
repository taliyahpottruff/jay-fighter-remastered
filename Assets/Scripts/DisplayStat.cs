using UnityEngine;
using UnityEngine.UI;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Text))]
[System.Obsolete("Implements a class that uses old Unity networking")]
public class DisplayStat : MonoBehaviour {
    public string statID;

    private Text text;

    private void Start() {
        text = GetComponent<Text>();
    }

    private void Update() {
        if (Game.STEAM != null) {
            int stat = PlayerPrefs.GetInt(statID);
            text.text = stat + "";
        }
    }
}