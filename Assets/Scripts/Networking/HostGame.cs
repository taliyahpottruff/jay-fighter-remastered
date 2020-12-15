using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: Taliyah Pottruff
/// </summary>

public class HostGame : MonoBehaviour {
    [SerializeField]
    private uint roomSize = 4;
    private string roomName;

    public MenuManager menuManager;
    public Transform playerListings;
    public Button hostGameButton;

    //private NetworkManager manager; Deprecated now, but was not deprecated at the time of development.

    private GameObject playerListingPrefab;

    private void Start() {
		//Initialize the network manager
        /*manager = NetworkManager.singleton;
        if (manager.matchMaker == null) {
            manager.StartMatchMaker();
        }*/

		//Set the player listing prefab from resources
        playerListingPrefab = Resources.Load<GameObject>("Prefabs/Player Listing");
    }

    /// <summary>
    /// Sets the room name of the match.
    /// </summary>
    /// <param name="name">The new name for the match</param>
    public void SetRoomName(string name) {
        if (!name.Equals("")) {
            hostGameButton.interactable = true;
            roomName = name;
        }
        else hostGameButton.interactable = false;
    }

    /// <summary>
    /// Creates a multiplayer match.
    /// </summary>
    public void CreateRoom() {
        if (roomName != "" && roomName != null) {
            Debug.Log("Creating Room:" + roomName + " with " + roomSize + " slots!");
            //Create Room, manager.OnMatchCreate
            Game.IS_MP = true;
            //manager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, manager.OnMatchCreate);
        }
    }

    public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo) {
        if (success) {
            Debug.Log("Success creating match '" + matchInfo.address + "' with extended info: " + extendedInfo);
            menuManager.ChangeMenu(3);
            Utilities.ClearChildren(playerListings);
            Instantiate(playerListingPrefab, playerListings);
        }
    }
}