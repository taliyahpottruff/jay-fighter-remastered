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

    public Sprite GetAvatar() {
        ulong steamID = client.SteamId;
        Image img = client.Friends.GetAvatar(Friends.AvatarSize.Medium, steamID);
        Texture2D tex = new Texture2D(img.Width, img.Height, TextureFormat.RGBA32, false);
        tex.LoadRawTextureData(img.Data);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, img.Height, img.Width, -img.Height), new Vector2(0, 0));
    }

    public void GiveAchievement(string id) {
        if (client.Achievements != null) {
            client.Achievements.Trigger(id);
        }
    }

    public int GetStat(string id) {
        if (client.Stats != null) {
            return client.Stats.GetInt(id);
        }

        return 0;
    }
}