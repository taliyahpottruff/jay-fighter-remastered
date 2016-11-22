using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class EditorMap : MonoBehaviour {
    public new string name = "New Map";
    public string json;

    private Map map;

    private void Start() {
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
        string directory = Application.persistentDataPath + "/maps";
        Debug.Log(directory);
        Directory.CreateDirectory(directory);
        FileStream fs = new FileStream(directory + "/" + map.name + ".map", FileMode.OpenOrCreate);
        using (StreamWriter writer = new StreamWriter(fs)) {
            writer.Write(json);
        }
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