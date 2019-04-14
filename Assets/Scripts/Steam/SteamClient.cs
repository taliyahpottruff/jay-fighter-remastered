using UnityEngine;
using Facepunch.Steamworks;

/*
 * AUTHOR: Trenton Potruff
 */

public class SteamClient {
    Client client;

    public SteamClient() {
        Config.ForUnity(Application.platform.ToString());
        client = new Client(665890);
        Debug.Log("Connected to Steam as \"" + client.Username + "\"...");
    }

	/// <summary>
	/// Gets the player's steam username.
	/// </summary>
	/// <returns>The player's steam username</returns>
    public string GetUsername() {
        return client.Username;
    }

	/// <summary>
	/// Gets the avatar of the player's steam profile.
	/// </summary>
	/// <returns></returns>
    public Sprite GetAvatar() {
        ulong steamID = client.SteamId;
        Image img = client.Friends.GetAvatar(Friends.AvatarSize.Medium, steamID);
        Texture2D tex = new Texture2D(img.Width, img.Height, TextureFormat.RGBA32, false);
        tex.LoadRawTextureData(img.Data);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, img.Height, img.Width, -img.Height), new Vector2(0, 0));
    }

	/// <summary>
	/// Gives the player an achievement.
	/// </summary>
	/// <param name="id">The ID of the achievment</param>
    public void GiveAchievement(string id) {
        if (client.Achievements != null) {
            client.Achievements.Trigger(id);
        }
    }
}