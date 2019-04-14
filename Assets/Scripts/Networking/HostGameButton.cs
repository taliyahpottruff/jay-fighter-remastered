using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HostGameButton : MonoBehaviour {
    private Button button;

    void Start() {
        button = GetComponent<Button>();        
    }

    public void CreateRoom() {
        GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<HostGame>().CreateRoom();
    }
}