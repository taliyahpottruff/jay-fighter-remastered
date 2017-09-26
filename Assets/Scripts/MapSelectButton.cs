using UnityEngine;
using UnityEngine.UI;

/*
 * AUTHOR: Trenton Pottruff
 */

public class MapSelectButton : MonoBehaviour {
    public Text title;
    public Text description;

    private MapSelectionDisplay display;

    private void Start() {
        display = GetComponentInParent<MapSelectionDisplay>();
    }

    public void SetInfo(string title, string description) {
        this.title.text = title;
        this.description.text = description;
    }

    /// <summary>
    /// Selects this map
    /// </summary>
    public void SelectMap() {
        Game.CURRENT_MAP = title.text;
    }

    /// <summary>
    /// Selects this map and begind the game
    /// </summary>
    public void SelectAndPlay() {
        SelectMap();
        display.PlayGame();
    }
}