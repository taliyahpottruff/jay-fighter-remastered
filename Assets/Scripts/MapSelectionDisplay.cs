﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MapSelectionDisplay : MonoBehaviour {
    public GameObject mapSelectButtonPrefab;

    private void Start() {
        PopulateList();
    }

    private void PopulateList() {
        Map[] maps = new Map[Game.MAPS.Count];
        Game.MAPS.Values.CopyTo(maps, 0);

        Utilities.ClearChildren(this.transform);
        for (int i = 0; i < Game.MAPS.Count; i++) {
            GameObject go = Instantiate(mapSelectButtonPrefab, this.transform) as GameObject;
            go.name = mapSelectButtonPrefab.name;
            MapSelectButton msb = go.GetComponent<MapSelectButton>();
            
            msb.SetInfo(maps[i].name, "This is a map!");
        }
    }

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }
}