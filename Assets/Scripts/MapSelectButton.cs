using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapSelectButton : MonoBehaviour {
    public Text title;
    public Text description;

    public void SetInfo(string title, string description) {
        this.title.text = title;
        this.description.text = description;
    }

    public void SelectMap() {
        Game.CURRENT_MAP = title.text;
    }
}