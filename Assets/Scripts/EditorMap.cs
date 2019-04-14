using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;

/*
 * AUTHOR: Trenton Pottruff
 */

public class EditorMap : MonoBehaviour {
    public new string name = "New Map";
    public string json;
    public MapSettingsPanel settingsPanel;

    private Map map;
    private string directory;

    private InputField mapNameField;

    private void Awake() {
        directory = Application.persistentDataPath + "/maps";
    }

    private void Start() {
        mapNameField = GameObject.FindGameObjectWithTag("MapNameInput").GetComponent<InputField>();

        UpdateMap(); //Init the JSON
    }

    /// <summary>
    /// Converts the current map configuration into JSON, and sets it to the json variable.
    /// </summary>
    public void UpdateMap() {
        map = new Map();
        map.name = name;
        GameObject[] gos = GameObject.FindGameObjectsWithTag("MapObj");
        MapObj[] objs = new MapObj[gos.Length];
        for (int i = 0; i < gos.Length; i++) {
            objs[i] = new MapObj();
            Transform t = gos[i].transform;
            objs[i].name = gos[i].name;
            objs[i].x = t.position.x;
            objs[i].y = t.position.y;
            objs[i].width = 1;
            objs[i].height = 1;

            MapEditorObject mapEditObj = gos[i].GetComponent<MapEditorObject>();
            objs[i].visible = mapEditObj.visible;
            objs[i].collider = mapEditObj.hasCollider;
            objs[i].spawn = mapEditObj.isSpawn;
        }
        map.objects = objs;

        json = JsonUtility.ToJson(map);
    }

    /// <summary>
    /// Changes the map name and updates the map.
    /// </summary>
    /// <param name="name">The new map name</param>
    public void SetMapName(string name) {
        this.name = name;
        UpdateMap();
    }

    /// <summary>
    /// Save the map to the "maps" folder.
    /// </summary>
    public void SaveMap() {
        Directory.CreateDirectory(directory);
        FileStream fs = new FileStream(directory + "/" + map.name + ".map", FileMode.OpenOrCreate);
        using (StreamWriter writer = new StreamWriter(fs)) {
            writer.Write(Utilities.Base64Encode(json)); //Write the encoded data
        }
        settingsPanel.ClosePanel();
    }

    /// <summary>
    /// Load a map from the "map" folder.
    /// </summary>
    /// <param name="mapToLoad">The name of the map to load</param>
    public void LoadMap(string mapToLoad) {
        FileStream fs = new FileStream(directory + "/" + mapToLoad + ".map", FileMode.Open);
        using (StreamReader reader = new StreamReader(fs)) {
            json = Utilities.Base64Decode(reader.ReadToEnd()); //Decodes the map
            map = JsonUtility.FromJson<Map>(json);
            GameObject mapGO = GameObject.FindGameObjectWithTag("Map");
            //Clears the map object of existing children
            for (int i = 0; i < mapGO.transform.childCount; i++) {
                Destroy(mapGO.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < map.objects.Length; i++) {
                MapObj mapObj = map.objects[i];
                GameObject prefab = Resources.Load<GameObject>("Prefabs/MapEditorObjects/" + mapObj.name);
                GameObject newGO = Instantiate(prefab, new Vector2(mapObj.x, mapObj.y), Quaternion.identity) as GameObject;
                newGO.name = prefab.name;
                newGO.transform.SetParent(mapGO.transform);
                Vector3 size = Vector3.one;
                size.x = mapObj.width;
                size.y = mapObj.height;
                newGO.transform.localScale = size;
                MapEditorObject newMapEditObj = newGO.GetComponent<MapEditorObject>();
                newMapEditObj.visible = mapObj.visible;
                newMapEditObj.hasCollider = mapObj.collider;
            }
            mapNameField.text = map.name;
        }
    }

    /// <summary>
    /// Load the map specified by the current "name" variable.
    /// </summary>
    public void LoadMap() {
        LoadMap(name);
    }
}