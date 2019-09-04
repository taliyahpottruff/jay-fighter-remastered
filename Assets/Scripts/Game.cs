using UnityEngine;
using System.Collections.Generic;
using System.IO;

/*
 * AUTHOR: Trenton Pottruff
 * CONTRIBUTOR: Garrett Nicholas
 * 
 * This is the main class for information storing.
 * Almost everything goes through here one way or another.
 */

[System.Obsolete("Implements a class that uses old Unity networking")]
public class Game
{
    public static bool PAUSED = false;
    public static float SFX_VOLUME = 0.25f;
    public static float MUSIC_VOLUME = 0.05f;
    public static string CURRENT_MAP = "Yard";
    public static bool IS_MP = false;
    public static SteamClient STEAM; //Steam Integration

    #region Maps
    public static Dictionary<string, Map> MAPS = new Dictionary<string, Map>() {
        {"Yard", new Map("Yard")},
        {"Lava Twins", new Map("Lava Twins")}
    };
    #endregion
    #region Items
    public static Dictionary<string, Item> ITEMS = new Dictionary<string, Item>() {
        {"Repair Kit", new HealthPotion()},
        {"Mine", new MineItem()},
        {"Plating Upgrade", new PlatingUpgrade()}
    };
    #endregion
    #region Stats
    public static Dictionary<string, int> STATS = new Dictionary<string, int>() {
        {"gamesPlayed", 0},
        {"score", 0},
        {"coins", 0},
        {"highestRound", 0}
    };
    #endregion

    /// <summary>
    /// Load whatever map is specified by the "CURRENT_MAP" global variable.
    /// </summary>
    /// <returns>The map</returns>
    public static Map LoadCurrentMap() {
        if (MAPS.ContainsKey(CURRENT_MAP))
            return MAPS[CURRENT_MAP];

        FileStream fs = new FileStream(Application.persistentDataPath + "/maps/" + CURRENT_MAP + ".map", FileMode.Open);
        using (StreamReader reader = new StreamReader(fs)) {
            string json = Utilities.Base64Decode(reader.ReadToEnd()); //Decode
            return JsonUtility.FromJson<Map>(json); //Parse
        }
    }

    /// <summary>
    /// Load a map based on a map ID.
    /// </summary>
    /// <param name="mapName">The map to load</param>
    /// <returns>The map</returns>
    public Map LoadMap(string mapName) {
        FileStream fs = new FileStream(Application.persistentDataPath + "/maps/" + mapName + ".map", FileMode.Open);
        using (StreamReader reader = new StreamReader(fs)) {
            string json = Utilities.Base64Decode(reader.ReadToEnd()); //Decode
            return JsonUtility.FromJson<Map>(json); //Parse
        }
    }

    /// <summary>
    /// Gets the current music volume.
    /// </summary>
    /// <returns>The music volume</returns>
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