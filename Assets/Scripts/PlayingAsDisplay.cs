using UnityEngine;
using UnityEngine.UI;

/*
 * AUTHOR: Trenton Pottruff
 */

/// <summary>
/// Displays to the user what Steam player is currently logged in.
/// </summary>
[System.Obsolete("Implements a class that uses old Unity networking")]
public class PlayingAsDisplay : MonoBehaviour {
    private Text text;
    [SerializeField]
    private Image avatar;

    private void Start() {
        text = GetComponent<Text>();
    }

    private void Update() {
        text.text = "Playing as " + Game.STEAM.GetUsername();
        avatar.sprite = Game.STEAM.GetAvatar();
    }
}