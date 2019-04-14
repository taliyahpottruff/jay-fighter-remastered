using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System;

/*
 * AUTHOR: Trenton Pottruff
 */

[Obsolete("Unity is removing the old networking features")]
public class JoinGame : MonoBehaviour {
    private List<GameObject> roomList = new List<GameObject>();
    private List<MatchInfoSnapshot> matchList = new List<MatchInfoSnapshot>();

    [SerializeField]
    Text status;
    [SerializeField]
    GameObject roomListItemPrefab;
    [SerializeField]
    Transform roomListParent;
    [SerializeField]
    Transform playerListings;
    GameObject playerListingPrefab;
    [SerializeField]
    MenuManager menuManager;

    private NetworkManager manager;

    private void Start() {
		//Initialize network manager
        manager = NetworkManager.singleton;
        if (manager.matchMaker == null) manager.StartMatchMaker();

		//Load player listing prefab from resources
        playerListingPrefab = Resources.Load<GameObject>("Prefabs/Player Listing");

		//Refresh the list of available room
        RefreshRoomList();
    }

	/// <summary>
	/// Refreshes the list of available games.
	/// </summary>
    public void RefreshRoomList() {
        ClearRoomList();
        manager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches) {
        matchList = matches;
        status.text = "";
        
		//If there was a problem grabbing the list, display an error message
        if (!success) {
            status.text = "Something went wrong...";
            return;
        }

        ClearRoomList();

		//Loop through the rooms and add them to the UI list
        foreach (var match in matches) {
            GameObject _roomListItemGO = Instantiate<GameObject>(roomListItemPrefab, roomListParent);

            //Set info
            Text _item = _roomListItemGO.GetComponentInChildren<Text>();
            _item.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";

            //Set RoomListItem
            _roomListItemGO.GetComponent<RoomListItem>().Setup(match, JoinRoom);

            roomList.Add(_roomListItemGO);
        }

        if (matches.Count < 1) {
            status.text = "No Games Available";
        }
    }

	/// <summary>
	/// Clears the UI list of rooms
	/// </summary>
    private void ClearRoomList() {
        for (int i = 0; i < roomList.Count; i++) {
            Destroy(roomList[i]);
        }

        roomList.Clear();
    }
	
    public void JoinRoom(MatchInfoSnapshot match) {
        Debug.Log("Joining " + match.name);
        Game.IS_MP = true;
        manager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, manager.OnMatchJoined);
        ClearRoomList();
        status.text = "Joining...";
    }
	
	public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo) {
        if (success) {
            menuManager.ChangeMenu(3);
            Utilities.ClearChildren(playerListings);
            foreach (NetworkConnection p in NetworkServer.connections) {
                GameObject go = Instantiate(playerListingPrefab, playerListings);
                Text t = go.GetComponentInChildren<Text>();
                t.text = p.address;
            }

            return;
        }

        Debug.LogError(extendedInfo);
    }
	
	public void JoinRandom() {
        RefreshRoomList();
        int rand = UnityEngine.Random.Range(0, matchList.Count);
        JoinRoom(matchList[rand]);
    }
}