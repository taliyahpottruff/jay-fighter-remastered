using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Game
{
    public static string CURRENT_MAP = "Basic";
    public static Dictionary<string, Map> MAPS = new Dictionary<string, Map>() {
        {"Basic", new Map("Basic", new MapObj[] {
            new MapObj("Test Object", 0, 0, 5, 5, true, false),
            new MapObj("Test Object", 10, 10, 1, 1)
        })}
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