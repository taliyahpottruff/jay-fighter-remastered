using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MatchButtons : MonoBehaviour {
    public NetworkManager manager;

    public void CreateMatch() {
        manager.StartMatchMaker();
        manager.matchMaker.CreateMatch("Tets", 2, true, "", "", "", 0, 1, manager.OnMatchCreate);
    }
}