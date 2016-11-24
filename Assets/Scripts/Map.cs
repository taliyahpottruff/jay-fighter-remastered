using System;

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
    public bool spawn;
}