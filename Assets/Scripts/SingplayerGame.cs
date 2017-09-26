using UnityEngine;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

public class SingplayerGame : MonoBehaviour {
    NetworkManager net;

    private void Start() {
        net = GetComponent<NetworkManager>();
        net.StartHost(); //Starts a single player game
    }
}