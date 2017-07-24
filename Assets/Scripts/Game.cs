using UnityEngine;
using System.Collections.Generic;
using System.IO;

/*
 * AUTHOR: Trenton Pottruff
 */

public class Game
{
    public static bool PAUSED = false;
    public static float SFX_VOLUME = 0.25f;
    public static float MUSIC_VOLUME = 0.05f;
    public static string CURRENT_MAP = "Basic";
    public static SteamClient STEAM;

    #region Maps
    public static Dictionary<string, Map> MAPS = new Dictionary<string, Map>() {
        {"Basic", new Map("Basic", 30, 30, new MapObj[] {
            //Objects
            new MapObj("Grass", -14.5f, 14.17f, 30f, 30f, true, false),
            //Colliders
                //Right
                new MapObj("Boulder", 15f, 15f, 1, 31, true, true),
                //Left
                new MapObj("Boulder", -15f, 15f, 1, 30, true, true),
                //Top
                new MapObj("Boulder", -15f, 15f, 30, 1, true, true),
                //Bottom
                new MapObj("Boulder", -15f, -15f, 31, 1, true, true),
            //Spawns
            new MapObj("Test Object", 4, 4, 1, 1),
            new MapObj("Test Object", 4, -4, 1, 1),
            new MapObj("Test Object", -4, 4, 1, 1),
            new MapObj("Test Object", -4, -4, 1, 1)
        })},
        {"Yard", new Map("Yard", 25, 40, new MapObj[] {
            //Objects
            new MapObj("Grass", -3.5f, 3.17f, 8, 8, true, false),
            //Colliders
            new MapObj("Boulder", 4f, 4f, 1, 3, true, true),
            new MapObj("Boulder", 4f, -2f, 1, 3, true, true),

            new MapObj("Boulder", 4f, 2f, 5, 1, true, true),
            new MapObj("Boulder", 4f, -2f, 5, 1, true, true),

            new MapObj("Boulder", -4f, 4f, 1, 3, true, true),
            new MapObj("Boulder", -4f, -2f, 1, 3, true, true),

            new MapObj("Boulder", -8f, 2f, 4, 1, true, true),
            new MapObj("Boulder", -8f, -2f, 4, 1, true, true),

            new MapObj("Boulder", -3f, -4f, 2, 1, true, true),
            new MapObj("Boulder", 2f, -4f, 2, 1, true, true),

            new MapObj("Boulder", -2f, -5f, 1, 2, true, true),
            new MapObj("Boulder", 2f, -5f, 1, 2, true, true),

            new MapObj("Boulder", -4f, 4f, 8, 1, true, true),
            //Spawns
            new MapObj("Test Object", -2, 16, 1, 1)
        })}
    };
    #endregion
    #region Items
    public static Dictionary<string, Item> ITEMS = new Dictionary<string, Item>() {
        {"Repair Kit", new HealthPotion()},
        {"Mine", new MineItem()}
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