using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

[RequireComponent(typeof(NetworkManager))]
public class MatchList : MonoBehaviour {
    public GameObject text;
    public GameObject matchSelectionPrefab;

    public NetworkManager manager;

    private void Start() {
        ListMatches();
    }

    public void ListMatches() {
        manager.StartMatchMaker();
        manager.SetMatchHost("mm.unet.unity3d.com", 443, true);

        manager.matchMaker.ListMatches(0, 20, "", false, 1, 0, OnMatchList);
    }

    public virtual void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) {
        if (success) {
            if (matchList.Count != 0) {
                Debug.Log("Matches Found");
                text.SetActive(false);
                //NetworkManager.singleton.matchMaker.JoinMatch(matchList[0].networkId, "", "", "", 0, 1, OnMatchJoined);
                foreach (var match in matchList) {
                    GameObject go = Instantiate<GameObject>(matchSelectionPrefab, this.transform);
                    Text matchName = go.GetComponentInChildren<Text>();
                    matchName.text = match.name;
                }
            }
            else {
                Debug.Log("No Matches Found");
                text.SetActive(true);
                //Debug.Log("Creating Match");
                //NetworkManager.singleton.matchMaker.CreateMatch("Match", 2, true, "", "", "", 0, 1, OnMatchCreate);
            }
        }
        else {
            Debug.Log("ERROR : Match Search Failure");
        }
    }
}