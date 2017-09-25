using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking.Match;

public class PauseScreenManager : MonoBehaviour {
    private bool opened = false;

    private Animator anim;

    private void Start() {
        //Grab animator component
        anim = GetComponent<Animator>();
    }

    public void OpenScreen() {
        anim.Play("pauseScreen-Open");
        opened = true;
        Game.PAUSED = true;
    }

    public void CloseScreen() {
        anim.Play("pauseScreen-Close");
        opened = false;
        Game.PAUSED = false;
    }

    public void CloseScreenWithoutUnpausing() {

    }
    public void ToggleScreen() {
        if (opened)
            CloseScreen();
        else
            OpenScreen();
    }

    public void ExitGame() {
        NetworkManager nm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NetworkManager>();
        nm.StopHost();

        SceneManager.LoadScene("MainMenu");
    }

    public void Disconnect() {
        NetworkManager nm = NetworkManager.singleton;
        MatchInfo match = nm.matchInfo;
        nm.matchMaker.DropConnection(match.networkId, match.nodeId, 0, nm.OnDropConnection);
        nm.StopHost();
    }

    public bool getOpened() {
        return opened;
    }
}
