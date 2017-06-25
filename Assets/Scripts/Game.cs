using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

public class Game
{
    public static bool PAUSED = false;
    public static float SFX_VOLUME = 0.25f;
    public static float MUSIC_VOLUME = 0.05f;
    public static string CURRENT_MAP = "Basic";
    public static SteamClient STEAM;

    #region Maps
    public static Dictionary<string, Map> MAPS = new Dictionary<string, Map>() {
        {"Basic", new Map("Basic", new MapObj[] {
            //Objects
            new MapObj("Grass", 0, -0.33f, 10.9f, 10.9f, true, false),
            //Colliders
                //Right
                new MapObj("Boulder", 5.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, -4.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, -3.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, -4.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, -2.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, -1.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, -0.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, 0.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, 1.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, 2.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, 3.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, 4.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, 5.5f, 1, 1, true, true),
                //Left
                new MapObj("Boulder", -5.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, -4.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, -3.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, -4.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, -2.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, -1.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, -0.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, 0.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, 1.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, 2.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, 3.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, 4.5f, 1, 1, true, true),
                new MapObj("Boulder", -5.5f, 5.5f, 1, 1, true, true),
                //Top
                new MapObj("Boulder", -5.5f, 5.5f, 1, 1, true, true),
                new MapObj("Boulder", -4.5f, 5.5f, 1, 1, true, true),
                new MapObj("Boulder", -3.5f, 5.5f, 1, 1, true, true),
                new MapObj("Boulder", -2.5f, 5.5f, 1, 1, true, true),
                new MapObj("Boulder", -1.5f, 5.5f, 1, 1, true, true),
                new MapObj("Boulder", -0.5f, 5.5f, 1, 1, true, true),
                new MapObj("Boulder", 0.5f, 5.5f, 1, 1, true, true),
                new MapObj("Boulder", 1.5f, 5.5f, 1, 1, true, true),
                new MapObj("Boulder", 2.5f, 5.5f, 1, 1, true, true),
                new MapObj("Boulder", 3.5f, 5.5f, 1, 1, true, true),
                new MapObj("Boulder", 4.5f, 5.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, 5.5f, 1, 1, true, true),
                //Bottom
                new MapObj("Boulder", -5.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", -4.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", -3.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", -2.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", -1.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", -0.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", 0.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", 1.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", 2.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", 3.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", 4.5f, -5.5f, 1, 1, true, true),
                new MapObj("Boulder", 5.5f, -5.5f, 1, 1, true, true),
            //Spawns
            new MapObj("Test Object", 4, 4, 1, 1),
            new MapObj("Test Object", 4, -4, 1, 1),
            new MapObj("Test Object", -4, 4, 1, 1),
            new MapObj("Test Object", -4, -4, 1, 1)
        })}
    };
    #endregion
    #region Items
    public static Dictionary<string, Item> ITEMS = new Dictionary<string, Item>() {
        {"Repair Kit", new HealthPotion()}
    };
    #endregion

    public static Map LoadCurrentMap() {
        if (MAPS.ContainsKey(CURRENT_MAP))
            return MAPS[CURRENT_MAP];

        FileStream fs = new FileStream(Application.persistentDataPath + "/maps/" + CURRENT_MAP + ".map", FileMode.Open);
        using (StreamReader reader = new StreamReader(fs)) {
            string json = Utilities.Base64Decode(reader.ReadToEnd());
            return JsonUtility.FromJson<Map>(json);
        }
    }

    public Map LoadMap(string mapName) {
        FileStream fs = new FileStream(Application.persistentDataPath + "/maps/" + mapName + ".map", FileMode.Open);
        using (StreamReader reader = new StreamReader(fs)) {
            string json = Utilities.Base64Decode(reader.ReadToEnd());
            return JsonUtility.FromJson<Map>(json);
        }
    }

    public static float GetMusicVolume() {
        if (!PlayerPrefs.HasKey("musicVolume")) { //If music volume is not saved into PlayerPrefs
            PlayerPrefs.SetFloat("musicVolume", MUSIC_VOLUME);
            PlayerPrefs.Save();
            return MUSIC_VOLUME;
        }

        MUSIC_VOLUME = PlayerPrefs.GetFloat("musicVolume");
        return MUSIC_VOLUME;
    }
}