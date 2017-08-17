using System;
using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[Serializable]
public class Map {
    public string name;
    public int width;
    public int height;
    public MapObj[] objects;

    public Map() {
        //Why is this blank?
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
        Texture2D floorImg = Resources.Load<Texture2D>(location + "-Floor"); //Load Floor Image for selected map
        Texture2D wallImg = Resources.Load<Texture2D>(location + "-Walls"); //Load Wall Image for selected map

        this.name = name;
        width = Math.Max(floorImg.width, wallImg.height); //Get the largest width of all the map images
        height = Math.Max(floorImg.height, wallImg.height); //Get the largest height of all the map images
        MapObj[] floorLayer = MapColorDictionary.ConvertImgToMapObjs(floorImg); //Get the floor objs from the image
        MapObj[] wallLayer = MapColorDictionary.ConvertImgToMapObjs(wallImg); //Get the wall objs from the image

        //Combine the layers
        int floorLayerCount = floorLayer.Length;
        int wallLayerCount = wallLayer.Length;
        int combinedCount = floorLayerCount + wallLayerCount; //Combined length of all layers
        MapObj[] combinedLayers = new MapObj[combinedCount];
        floorLayer.CopyTo(combinedLayers, 0); //Copy floor layer to the combined layer array
        wallLayer.CopyTo(combinedLayers, floorLayerCount); //Copy the wall layer to the combined layer array
        objects = combinedLayers; //Set the actual object array equal to the new combined layer array
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