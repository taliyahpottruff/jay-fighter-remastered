using UnityEngine;
using System.Collections.Generic;

/*
 * AUTHOR: Trenton Pottruff
 * Helper class for converting an image into a map using coloured pixels.
*/
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
        new MapColor(new Color(1f, 106f/255f, 0f), "Lava"),
        new MapColor(new Color(17f/255f, 17f/255f, 17f/255f), "Gravel"),
        new MapColor(new Color(30f/255f, 30f/255f, 30f/255f), "Gravel_Left"),
        new MapColor(new Color(31f/255f, 31f/255f, 31f/255f), "Gravel_Right"),
        new MapColor(new Color(32f/255f, 32f/255f, 32f/255f), "Gravel_Top"),
        new MapColor(new Color(33f/255f, 33f/255f, 33f/255f), "Gravel_Bottom"),
        new MapColor(new Color(34f/255f, 34f/255f, 34f/255f), "Gravel_BRCorner"),
        new MapColor(new Color(35f/255f, 35f/255f, 35f/255f), "Gravel_BLCorner"),
        new MapColor(new Color(36f/255f, 36f/255f, 36f/255f), "Gravel_TLCorner"),
        new MapColor(new Color(37f/255f, 37f/255f, 37f/255f), "Gravel_TRCorner"),
        new MapColor(new Color(38f/255f, 38f/255f, 38f/255f), "Gravel_TopLeft"),
        new MapColor(new Color(39f/255f, 39f/255f, 39f/255f), "Gravel_TopRight"),
        new MapColor(new Color(40f/255f, 40f/255f, 40f/255f), "Gravel_BottomRight"),
        new MapColor(new Color(41f/255f, 41f/255f, 41f/255f), "Gravel_BottomLeft"),
        new MapColor(new Color(42f/255f, 42f/255f, 42f/255f), "Gravel_Edge"),
        new MapColor(new Color(255f/255f, 255f/255f, 50f/255f), "Lava_BCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 1f/255f), "Lava_TopCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 2f/255f), "Lava_LeftCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 3f/255f), "Lava_RightCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 4f/255f), "Lava_TLCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 5f/255f), "Lava_TRCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 6f/255f), "Lava_BLCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 7f/255f), "Lava_BRCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 8f/255f), "Lava_TLCCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 9f/255f), "Lava_TRCCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 10f/255f), "Lava_BLCCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 11f/255f), "Lava_BRCCliff"),
        new MapColor(new Color(255f/255f, 255f/255f, 12f/255f), "Lava_RightSpecial"),
        new MapColor(new Color(255f/255f, 255f/255f, 13f/255f), "Lava_LeftSpecial"),
        new MapColor(new Color(255f/255f, 255f/255f, 14f/255f), "Lava_TopSpecial"),
        new MapColor(new Color(255f/255f, 255f/255f, 15f/255f), "Lava_BottomSpecial"),
        new MapColor(new Color(255f/255f, 255f/255f, 16f/255f), "Lava_RightEdges"),
        new MapColor(new Color(255f/255f, 255f/255f, 17f/255f), "Lava_LeftEdges"),
        new MapColor(new Color(255f/255f, 255f/255f, 18f/255f), "Lava_TopEdges"),
        new MapColor(new Color(255f/255f, 255f/255f, 19f/255f), "Lava_BottomEdges")
    };

	/// <summary>
	/// Convert a color to an object
	/// </summary>
	/// <param name="c">The color to convert</param>
	/// <returns>The corresponding object. Return a blank ID if no Color matches.</returns>
    public static string ColorToID(Color c) {
		//Loop through the map colors to find the requested color
        for (int i = 0; i < mapColors.Length; i++) {
            if (mapColors[i].color.Equals(c))
                return mapColors[i].objID;
        }

        return "";
    }

	/// <summary>
	/// Convert an image to an array of map objects.
	/// </summary>
	/// <param name="texture">The image to convert</param>
	/// <returns>The array of map objects</returns>
    public static MapObj[] ConvertImgToMapObjs(Texture2D texture) {
        List<MapObj> objects = new List<MapObj>();

		//Loop through each pixel in the image
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

/// <summary>
/// Simple data holder
/// </summary>
public class MapColor {
    public Color color;
    public string objID;

    public MapColor(Color color, string objID) {
        this.color = color;
        this.objID = objID;
    }
}