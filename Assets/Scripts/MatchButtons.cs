using UnityEngine;
using UnityEngine.Networking;

//TODO I think this is another class that needs to be deleted
public class MatchButtons : MonoBehaviour {
    public NetworkManager manager;

    public void CreateMatch() {
        manager.StartMatchMaker();
        manager.matchMaker.CreateMatch("Tets", 2, true, "", "", "", 0, 1, manager.OnMatchCreate);
    }
}