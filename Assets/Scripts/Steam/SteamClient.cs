using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facepunch.Steamworks;

public class SteamClient {
    Client client;

    public SteamClient() {
        Config.ForUnity(Application.platform.ToString());
        client = new Client(665890);
        Debug.Log("Connected to Steam as \"" + client.Username + "\"...");
    }

    public string GetUsername() {
        return client.Username;
    }
}