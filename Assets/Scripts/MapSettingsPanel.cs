using UnityEngine;
using System.Collections;

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

    public void TogglePanel() {
        if (mode == PanelMode.Open)
            ClosePanel();
        else
            OpenPanel();
    }

    public void OpenPanel() {
        anim.Play("settingsPanel-Open");
        mode = PanelMode.Open;
    }

    public void ClosePanel() {
        anim.Play("settingsPanel-Close");
        mode = PanelMode.Close;
    }
}
