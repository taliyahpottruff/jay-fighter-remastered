using UnityEngine;

/*
 * AUTHOR: Trenton Pottruf
*/

public class SetupSteam : MonoBehaviour {
    private void Start() {
        Game.STEAM = new SteamClient();
    }
}