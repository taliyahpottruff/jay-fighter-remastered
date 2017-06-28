using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupSteam : MonoBehaviour {
    private void Start() {
        Game.STEAM = new SteamClient();
    }
}