using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class HostGame : MonoBehaviour {
    [SerializeField]
    private uint roomSize = 4;
    private string roomName;

    public MenuManager menuManager;
    public Transform playerListings;

    private NetworkManager manager;

    private GameObject playerListingPrefab;

    private void Start() {
        manager = NetworkManager.singleton;
        if (manager.matchMaker == null) {
            manager.StartMatchMaker();
        }

        playerListingPrefab = Resources.Load<GameObject>("Prefabs/Player Listing");
    }

    public void SetRoomName(string name) {
        roomName = name;
    }

    public void CreateRoom() {
        if (roomName != "" && roomName != null) {
            Debug.Log("Creating Room:" + roomName + " with " + roomSize + " slots!");
            //Create Room, manager.OnMatchCreate
            manager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, manager.OnMatchCreate);
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

    public void StartGame() {
        manager.ServerChangeScene("Coop");
    }
}