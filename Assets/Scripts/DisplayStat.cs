using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayStat : MonoBehaviour {
    public string statID;

    private Text text;

    private void Start() {
        text = GetComponent<Text>();
    }

    private void Update() {
        if (Game.STEAM != null) {
            int stat = Game.STEAM.GetStat(statID);
            Game.STATS[statID] = stat;
            text.text = stat + "";
        }
    }
}