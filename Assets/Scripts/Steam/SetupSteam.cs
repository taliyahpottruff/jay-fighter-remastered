using UnityEngine;

/*
 * AUTHOR: Trenton Pottruf
*/

[System.Obsolete("Implements a class that uses old Unity networking")]
public class SetupSteam : MonoBehaviour {
    private void Start() {
        Game.STEAM = new SteamClient();
    }
}