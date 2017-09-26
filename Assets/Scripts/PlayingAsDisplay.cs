using UnityEngine;
using UnityEngine.UI;

/*
 * AUTHOR: Trenton Pottruff
 */

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