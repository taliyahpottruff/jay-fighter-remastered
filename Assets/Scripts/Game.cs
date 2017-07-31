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
        #region Basic Map
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
        #endregion
        {"Yard", new Map("Yard", 25, 40, new MapObj[] {
            //Objects
            new MapObj("Grass", -3.5f, 3.17f, 8, 8, true, false),
            new MapObj("Grass", -8, 1.17f, 5, 4, true, false),
            new MapObj("Grass", 4, 1.17f, 5, 4, true, false),
            new MapObj("Grass", -11.5f, 9.17f, 4, 20, true, false),
            new MapObj("Grass", -8.5f, -6.83f, 21, 4, true, false),
            new MapObj("Grass", 8.5f, 9.17f, 4, 17, true, false),
            new MapObj("Grass", -8.5f, 9.17f, 18, 4, true, false),
            new MapObj("Grass", -1.5f, 12, 4, 3, true, false),
            new MapObj("Grass", -2.5f, 13.17f, 6, 2, true, false),
            new MapObj("Grass", -3.5f, 17.17f, 8, 5, true, false),
            new MapObj("Grass", -2.5f, 18.17f, 6, 2, true, false),
            new MapObj("Grass", -1.5f, -4, 4, 3, true, false),
            new MapObj("Grass", -7.5f, -10, 4, 3, true, false),
            new MapObj("Grass", 4.5f, -10, 4, 3, true, false),
            new MapObj("Grass", -7.5f, -12.83f, 16, 8, true, false),
            //Walls
            new MapObj("Boulder", 4f, 4f, 1, 3, true, false),
            new MapObj("Boulder", 4f, -2f, 1, 3, true, false),

            new MapObj("Boulder", 4f, 2f, 5, 1, true, false),
            new MapObj("Boulder", 4f, -2f, 5, 1, true, false),

            new MapObj("Boulder", -4f, 4f, 1, 3, true, false),
            new MapObj("Boulder", -4f, -2f, 1, 3, true, false),

            new MapObj("Boulder", -8f, 2f, 4, 1, true, false),
            new MapObj("Boulder", -8f, -2f, 4, 1, true, false),

            new MapObj("Boulder", -3f, -4f, 2, 1, true, false),
            new MapObj("Boulder", 2f, -4f, 2, 1, true, false),

            new MapObj("Boulder", -2f, -5f, 1, 2, true, false),
            new MapObj("Boulder", 2f, -5f, 1, 2, true, false),

            new MapObj("Boulder", -4f, -10f, 9, 1, true, false),
            new MapObj("Boulder", -4, -11, 1, 2, true, false),
            new MapObj("Boulder", 4, -11, 1, 2, true, false),
            new MapObj("Boulder", -3, -12, 7, 1, true, false),

            new MapObj("Boulder", -12, 10, 1, 21, true, false),
            new MapObj("Boulder", -11, 10, 10, 1, true, false),
            new MapObj("Boulder", -2, 12, 1, 2, true, false),
            new MapObj("Boulder", -3, 13, 1, 2, true, false),
            new MapObj("Boulder", -4, 18, 1, 6, true, false),
            new MapObj("Boulder", -3, 19, 1, 2, true, false),
            new MapObj("Boulder", -2, 19, 6, 1, true, false),
            new MapObj("Boulder", 3, 18, 2, 1, true, false),
            new MapObj("Boulder", 4, 17, 1, 5, true, false),
            new MapObj("Boulder", 3, 13, 1, 2, true, false),
            new MapObj("Boulder", 2, 12, 1, 3, true, false),
            new MapObj("Boulder", 3, 10, 10, 1, true, false),
            new MapObj("Boulder", 12, 9, 1, 20, true, false),
            new MapObj("Boulder", 8, -10, 4, 1, true, false),
            new MapObj("Boulder", 8, -11, 1, 10, true, false),
            new MapObj("Boulder", -8, -20, 16, 1, true, false),
            new MapObj("Boulder", -8, -10, 1, 10, true, false),
            new MapObj("Boulder", -12, -10, 4, 1, true, false),

            new MapObj("Boulder", -8, 6, 17, 1, true, false),
            new MapObj("Boulder", -8, 5, 1, 3, true, false),
            new MapObj("Boulder", 8, 5, 1, 3, true, false),

            new MapObj("Boulder", -8, -3, 1, 4, true, false),
            new MapObj("Boulder", -7, -6, 5, 1, true, false),

            new MapObj("Boulder", 3, -6, 6, 1, true, false),
            new MapObj("Boulder", 8, -3, 1, 3, true, false),


            new MapObj("Boulder", -4f, 4f, 8, 1, true, false),
            //Colliders
            new MapObj("Collider", 4f, 3f, 1, 3, false, true),
            new MapObj("Collider", 4f, -3f, 1, 3, false, true),
            new MapObj("Collider", 6f, 2f, 5, 1, false, true),
            new MapObj("Collider", 6f, -2f, 5, 1, false, true),
            new MapObj("Collider", -4f, 3f, 1, 3, false, true),
            new MapObj("Collider", -4f, -3f, 1, 3, false, true),
            new MapObj("Collider", -6.5f, 2f, 4, 1, false, true),
            new MapObj("Collider", -6.5f, -2f, 4, 1, false, true),
            new MapObj("Collider", -2.5f, -4f, 2, 1, false, true),
            new MapObj("Collider", 2.5f, -4f, 2, 1, false, true),
            new MapObj("Collider", -2f, -5.5f, 1, 2, false, true),
            new MapObj("Collider", 2f, -5.5f, 1, 2, false, true),
            new MapObj("Collider", 0f, -10f, 9, 1, false, true),
            new MapObj("Collider", -4, -11.5f, 1, 2, false, true),
            new MapObj("Collider", 4, -11.5f, 1, 2, false, true),
            new MapObj("Collider", 0, -12, 7, 1, false, true),
            new MapObj("Collider", -12, 0, 1, 21, false, true),
            new MapObj("Collider", -6.5f, 10, 10, 1, false, true),
            new MapObj("Collider", -2, 11, 1, 3, false, true),
            new MapObj("Collider", -3, 12.5f, 1, 2, false, true),
            new MapObj("Collider", -4, 15, 1, 6, false, true),
            new MapObj("Collider", -3, 18.5f, 1, 2, false, true),
            new MapObj("Collider", 0, 19, 7, 1, false, true),
            new MapObj("Collider", 3.5f, 18, 2, 1, false, true),
            new MapObj("Collider", 4, 15, 1, 6, false, true),
            new MapObj("Collider", 3, 12.5f, 1, 2, false, true),
            new MapObj("Collider", 2, 11, 1, 3, false, true),
            new MapObj("Collider", 7.5f, 10, 10, 1, false, true),
            new MapObj("Collider", 12, 0, 1, 20, false, true),
            new MapObj("Collider", 10, -10, 5, 1, false, true),
            new MapObj("Collider", 8, -15.5f, 1, 10, false, true),
            new MapObj("Collider", 0, -20, 16, 1, false, true),
            new MapObj("Collider", -8, -15, 1, 11, false, true),
            new MapObj("Collider", -10, -10, 4, 1, false, true),
            new MapObj("Collider", 0, 6, 17, 1, false, true),
            new MapObj("Collider", -8, 4, 1, 3, false, true),
            new MapObj("Collider", 8, 4, 1, 3, false, true),
            new MapObj("Collider", -8, -4.5f, 1, 4, false, true),
            new MapObj("Collider", -5, -6, 5, 1, false, true),
            new MapObj("Collider", 5.5f, -6, 6, 1, false, true),
            new MapObj("Collider", 8, -4, 1, 3, false, true),
            new MapObj("Collider", 0, 4f, 8, 1, false, true),
            //Spawns
            new MapObj("Test Object", -2, 16, 1, 1),
            new MapObj("Test Object", 2, 16, 1, 1),
            new MapObj("Test Object", -6, -8, 1, 1),
            new MapObj("Test Object", 6, -8, 1, 1),
            new MapObj("Test Object", 0, -5, 1, 1),
            new MapObj("Test Object", -3, -14, 1, 1),
            new MapObj("Test Object", 3, -14, 1, 1),
            new MapObj("Test Object", -5, -17, 1, 1),
            new MapObj("Test Object", 5, -17, 1, 1)
        })}
    };
    #endregion
    #region Items
    public static Dictionary<string, Item> ITEMS = new Dictionary<string, Item>() {
        {"Repair Kit", new HealthPotion()},
        {"Mine", new MineItem()},
        {"Plating Upgrade", new PlatingUpgrade()}
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