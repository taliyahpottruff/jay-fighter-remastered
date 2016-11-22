using UnityEngine;
using System.Collections;

public class LoadMapOption : MonoBehaviour {
    public string mapToLoad;
    public LoadMapPanel panel;

    private EditorMap map;

    private void Start() {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<EditorMap>();
    }

    public void TriggerMapLoad() {
        map.LoadMap(mapToLoad);
        panel.HidePanel();
    }
}
