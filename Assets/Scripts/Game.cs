using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

public class Game
{
    public static bool PAUSED = false;
    public static float SFX_VOLUME = 0.5f;
    public static string CURRENT_MAP = "Basic";

    public static Dictionary<string, Map> MAPS = new Dictionary<string, Map>() {
        {"Basic", new Map("Basic", new MapObj[] {
            //Objects
            new MapObj("Grass", 0, 0, 10, 10, true, false),
            //Colliders
            new MapObj("Test Object", 5.5f, 0, 1, 11, true, true),
            new MapObj("Test Object", -5.5f, 0, 1, 11, true, true),
            new MapObj("Test Object", 0, 5.5f, 11, 1, true, true),
            new MapObj("Test Object", 0,-5.5f, 11, 1, true, true),
            //Spawns
            new MapObj("Test Object", 5, 5, 1, 1),
            new MapObj("Test Object", 5, -5, 1, 1),
            new MapObj("Test Object", -5, 5, 1, 1),
            new MapObj("Test Object", -5, -5, 1, 1)
        })}
    };
    public static Dictionary<string, Item> ITEMS = new Dictionary<string, Item>() {
        {"Health Potion", new HealthPotion()}
    };

    public static Map LoadCurrentMap() {
        return MAPS[CURRENT_MAP];
    }

    public void LoadMap(string mapName) {
        FileStream fs = new FileStream(Application.persistentDataPath + "/maps/" + mapName + ".map", FileMode.Open);
        using (StreamReader reader = new StreamReader(fs)) {
            string json = Utilities.Base64Decode(reader.ReadToEnd());
            MAPS[mapName] = JsonUtility.FromJson<Map>(json);
        }
    }
}