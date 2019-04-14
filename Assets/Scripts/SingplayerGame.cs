using UnityEngine;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

public class SingplayerGame : MonoBehaviour {
    NetworkManager net;

    public GameMap map;

    private void Start() {
        net = GetComponent<NetworkManager>();
        net.StartHost(); //Starts a single player game
        //map.StartMap();
    }
}