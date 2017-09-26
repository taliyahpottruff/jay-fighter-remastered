using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Animator))]
public class MapSettingsPanel : MonoBehaviour {
    public PanelMode mode = PanelMode.Close;

    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Toggles the panel in and out
    /// </summary>
    public void TogglePanel() {
        if (mode == PanelMode.Open)
            ClosePanel();
        else
            OpenPanel();
    }

    /// <summary>
    /// Opens the panel regardless of its current status
    /// </summary>
    public void OpenPanel() {
        anim.Play("settingsPanel-Open");
        mode = PanelMode.Open;
    }

    /// <summary>
    /// Clsoes the panel regardless of its current status
    /// </summary>
    public void ClosePanel() {
        anim.Play("settingsPanel-Close");
        mode = PanelMode.Close;
    }

    /// <summary>
    /// Returns the user to the main menu
    /// </summary>
    public void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
