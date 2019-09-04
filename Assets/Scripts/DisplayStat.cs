using UnityEngine;
using UnityEngine.UI;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Text))]
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