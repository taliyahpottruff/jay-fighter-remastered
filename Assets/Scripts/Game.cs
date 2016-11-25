using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game
{
    public static string CURRENT_MAP = "Basic";
    public static Dictionary<string, Map> MAPS = new Dictionary<string, Map>() {
        {"Basic", new Map("Basic", new MapObj[] {
            new MapObj("TestObj", 0, 0, 5, 5, true, false)
        })}
    };

    public static Map LoadCurrentMap() {
        return MAPS[CURRENT_MAP];
    }
}