using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {
    [SerializeField]
    private uint roomSize = 4;
    private string roomName;

    private NetworkManager manager;

    private void Start() {
        manager = NetworkManager.singleton;
        if (manager.matchMaker == null) {
            manager.StartMatchMaker();
        }
    }

    public void SetRoomName(string name) {
        roomName = name;
    }

    public void CreateRoom() {
        if (roomName != "" && roomName != null) {
            Debug.Log("Creating Room:" + roomName + " with " + roomSize + " slots!");
            //Create Room
            manager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, manager.OnMatchCreate);
        }
    }
}