using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SingplayerGame : MonoBehaviour {
    NetworkManager net;

    private void Start() {
        net = GetComponent<NetworkManager>();
        net.StartHost();
    }
}
