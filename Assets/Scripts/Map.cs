using System;
using UnityEngine;

[Serializable]
public class Map {
    public string name;
    public int width;
    public int height;
    public MapObj[] objects;

    public Map() {

    }
    
    public Map(string name, int width, int height, MapObj[] objects) {
        this.width = width;
        this.height = height;
        this.name = name;
        this.objects = objects;
    }

    public Map(string name) {
        //Load Map For Image
        string location = "Maps/" + name;
        Texture2D mapImg = Resources.Load<Texture2D>(location);

        this.name = name;
        width = mapImg.width;
        height = mapImg.height;
        objects = MapColorDictionary.ConvertImgToMapObjs(mapImg);
        Debug.LogWarning(objects.Length);
    }
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
    public bool spawn;

    public MapObj() {

    }

    public MapObj(string name, float x, float y, float width, float height, bool visible, bool collider) {
        this.name = name;
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.visible = visible;
        this.collider = collider;
        spawn = false;
    }

    public MapObj(string name, float x, float y, float width, float height) {
        this.name = name;
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        visible = false;
        collider = false;
        spawn = true;
    }
}