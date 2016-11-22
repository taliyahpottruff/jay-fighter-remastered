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

    private Map map;
    private string directory = Application.persistentDataPath + "/maps";

    private InputField mapNameField;

    private void Start() {
        mapNameField = GameObject.FindGameObjectWithTag("MapNameInput").GetComponent<InputField>();

        UpdateMap(); //Init the JSON
    }

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
            objs[i].width = t.localScale.x;
            objs[i].height = t.localScale.y;

            MapEditorObject mapEditObj = gos[i].GetComponent<MapEditorObject>();
            objs[i].visible = mapEditObj.visible;
            objs[i].collider = mapEditObj.hasCollider;
        }
        map.objects = objs;

        json = JsonUtility.ToJson(map);
        Debug.Log(json);
    }

    public void SetMapName(string name) {
        this.name = name;
        UpdateMap();
    }

    public void SaveMap() {
        Directory.CreateDirectory(directory);
        FileStream fs = new FileStream(directory + "/" + map.name + ".map", FileMode.OpenOrCreate);
        using (StreamWriter writer = new StreamWriter(fs)) {
            writer.Write(json);
        }
    }

    public void LoadMap(string mapToLoad) {
        FileStream fs = new FileStream(directory + "/" + mapToLoad + ".map", FileMode.Open);
        using (StreamReader reader = new StreamReader(fs)) {
            json = reader.ReadToEnd();
            map = JsonUtility.FromJson<Map>(json);
            GameObject mapGO = GameObject.FindGameObjectWithTag("Map");
            //Clears the map object of existing children
            for (int i = 0; i < mapGO.transform.childCount; i++) {
                Destroy(mapGO.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < map.objects.Length; i++) {
                MapObj mapObj = map.objects[i];
                GameObject prefab = Resources.Load<GameObject>("Prefabs/MapEditorObjects/" + mapObj.name);
                GameObject newGO = Instantiate(prefab, new Vector2(mapObj.x, mapObj.y), Quaternion.identity, mapGO.transform) as GameObject;
                newGO.name = prefab.name;
                Vector3 size = Vector3.one;
                size.x = mapObj.width;
                size.y = mapObj.height;
                newGO.transform.localScale = size;
                MapEditorObject newMapEditObj = newGO.GetComponent<MapEditorObject>();
                newMapEditObj.visible = mapObj.visible;
                newMapEditObj.hasCollider = mapObj.collider;
            }
        }
    }

    public void LoadMap() {
        LoadMap(name);
    }
}

[Serializable]
public class Map {
    public string name;
    public MapObj[] objects;
}

[Serializable]
public class MapObj {
    public string name;
    public float x;
    public float y;
    public float width;
    public float height;
    public bool visible;
    public bool collider;
}