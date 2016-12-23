using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    public void SelectMap() {
        Game.CURRENT_MAP = title.text;
    }

    public void SelectAndPlay() {
        SelectMap();
        display.PlayGame();
    }
}