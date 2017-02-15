using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
        SceneManager.LoadScene("MainMenu");
    }
}