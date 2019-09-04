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

    /// <summary>
    /// Sets the info for the button
    /// </summary>
    /// <param name="title">The title of the map</param>
    /// <param name="description">The description of the map</param>
    /// <param name="buttonIndex">The index of the button</param>
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
    /// Selects this map and starts the game
    /// </summary>
    public void SelectAndPlay() {
        SelectMap();
        Game.IS_MP = false;
        display.PlayGame();
    }
}