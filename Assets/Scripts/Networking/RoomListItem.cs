using System;
using UnityEngine;
using UnityEngine.Networking.Match;

/*
 * AUTHOR: Trenton Pottruff
*/

[Obsolete("Unity is removing the old networking features")]
public class RoomListItem : MonoBehaviour {
    public delegate void JoinGameDelegate(MatchInfoSnapshot _match);
    private JoinGameDelegate joinGameCallback;

    private MatchInfoSnapshot match;

    public void Setup(MatchInfoSnapshot _match, JoinGameDelegate _joinGameCallback) {
        match = _match;
        joinGameCallback = _joinGameCallback;
    }

    public void JoinGame() {
        joinGameCallback.Invoke(match);
    }
}