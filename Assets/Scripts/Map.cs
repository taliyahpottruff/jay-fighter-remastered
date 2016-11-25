using System;

[Serializable]
public class Map {
    public string name;
    public MapObj[] objects;

    public Map() {

    }
    
    public Map(string name, MapObj[] objects) {
        this.name = name;
        this.objects = objects;
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