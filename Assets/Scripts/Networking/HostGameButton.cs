using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(Button))]
[Obsolete("Refrences a class that uses old Unity networking.")]
public class HostGameButton : MonoBehaviour {
    private Button button;

    void Start() {
        button = GetComponent<Button>();        
    }

    public void CreateRoom() {
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<HostGame>().CreateRoom();
    }
}