using UnityEngine;
using System.Collections.Generic;

public class MapColorDictionary {
    private static MapColor[] mapColors = new MapColor[] {
        new MapColor(new Color(1, 1, 0), "Enemy Spawn"),
        new MapColor(new Color((112f/255f), 112f/255f, 112f/255f), "Boulder"),
        new MapColor(new Color(38f/255f, 130f/255f, 40f/255f), "Grass"),
        new MapColor(new Color(91f/255f, 127f/255f, 0f), "Grass_Top_1"),
        new MapColor(new Color(127f/255f, 106f/255f, 0f), "Grass_Bottom_1"),
        new MapColor(new Color(91f/255f, 107f/255f, 0f), "Grass_Left_1"),
        new MapColor(new Color(69f/255f, 81f/255f, 0f), "Grass_Right_1"),
        new MapColor(new Color(127f/255f, 0f, 0f), "Grass_TopLeft_1"),
        new MapColor(new Color(61f/255f, 42f/255f, 0f), "Grass_TopRight_1"),
        new MapColor(new Color(0f, 127f/255f, 70f/255f), "Grass_BottomLeft_1"),
        new MapColor(new Color(0f, 168f/255f, 92f/255f), "Grass_BottomRight_1"),
        new MapColor(new Color(182f/255f, 1f, 0f), "Grass_TLCorner_1"),
        new MapColor(new Color(183f/255f, 1f, 0f), "Grass_TRCorner_1"),
        new MapColor(new Color(184f/255f, 1f, 0f), "Grass_BRCorner_1"),
        new MapColor(new Color(185f/255f, 1f, 0f), "Grass_BLCorner_1"),
        new MapColor(new Color(1f, 106f/255f, 0f), "Lava")
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
                    bool visible = true;
                    bool collider = false;

                    if (id.Equals("Boulder")) { //Add any IDs here that you want to have colliders
                        collider = true;
                    }

                    //Check if the ID is an Enemy Spawn, add object normally if not
                    if (id.Equals("Enemy Spawn")) {
                        objects.Add(new MapObj("Test Object", -(texture.width / 2) + x, -(texture.height / 2) + y, 1, 1));
                    }
                    else {
                        objects.Add(new MapObj(id, -(texture.width / 2) + x, -(texture.height / 2) + y, 1, 1, visible, collider));
                    }
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