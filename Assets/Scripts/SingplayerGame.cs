using UnityEngine;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

//Class name was mispelled and I never changed it.
[System.Obsolete("Uses Unity's old networking features")]
public class SingplayerGame : MonoBehaviour {
    NetworkManager net;

    public GameMap map;

    private void Start() {
        net = GetComponent<NetworkManager>();
        net.StartHost(); //Starts a single player game
        //map.StartMap();
    }
}