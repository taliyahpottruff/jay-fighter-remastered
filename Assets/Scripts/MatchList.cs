using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * AUTHOR: Trenton Pottruff
 */

//[RequireComponent(typeof(NetworkManager))]
public class MatchList : MonoBehaviour {
    public GameObject text;
    public GameObject matchSelectionPrefab;

    //public NetworkManager manager;

    private void Start() {
        ListMatches();
    }

    public void ListMatches() {
        /*manager.StartMatchMaker();
        manager.SetMatchHost("mm.unet.unity3d.com", 443, true);

        manager.matchMaker.ListMatches(0, 20, "", false, 1, 0, OnMatchList);*/
    }

    /*public virtual void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) {
        if (success) {
            if (matchList.Count != 0) {
                Debug.Log("Matches Found");
                text.SetActive(false);
                foreach (var match in matchList) {
                    GameObject go = Instantiate<GameObject>(matchSelectionPrefab, this.transform);
                    Text matchName = go.GetComponentInChildren<Text>();
                    matchName.text = match.name;
                }
            }
            else {
                Debug.Log("No Matches Found");
                text.SetActive(true);
            }
        }
        else {
            Debug.Log("ERROR : Match Search Failure");
        }
    }*/
}