using UnityEngine;
using System.Collections.Generic;

public class MapColorDictionary {
    private static MapColor[] mapColors = new MapColor[] {
        new MapColor(new Color(1, 1, 0), "Enemy Spawn"),
        new MapColor(new Color((112f/255f), 112f/255f, 112f/255f), "Boulder"),
        new MapColor(new Color(38f/255f, 130f/255f, 40f/255f), "Grass")
    };

    public static string ColorToID(Color c) {
        for (int i = 0; i < mapColors.Length; i++) {
            if (mapColors[i].color.Equals(c))
                return mapColors[i].objID;
        }

        return "";
    }

    public static MapObj[] ConvertImgToMapObjs(Texture2D texture) {
        List<MapObj> objects = new List<MapObj>();

        Debug.Log(texture.width);

        for (int x = 0; x < texture.width; x++) {
            for (int y = 0; y < texture.height; y++) {
                Color c = texture.GetPixel(x, y);
                string id = ColorToID(c);
                if (!id.Equals("")) {
                    objects.Add(new MapObj(id, -(texture.width/2)+x, -(texture.height/2)+y, 1, 1, true, false));
                }
            }
        }

        return objects.ToArray();
    }
}

public class MapColor {
    public Color color;
    public string objID;

    public MapColor(Color color, string objID) {
        this.color = color;
        this.objID = objID;
    }
}