using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class AddObjectPanel : MonoBehaviour {
    public PanelMode mode = PanelMode.Close;
    public GameObject expandButton;

    private Animator anim;
    private Text expandButtonText;

    private void Start() {
        anim = GetComponent<Animator>();
        expandButtonText = expandButton.GetComponentInChildren<Text>();
    }

    public void TogglePanel() {
        if (mode == PanelMode.Open)
            ClosePanel();
        else
            OpenPanel();
    }

    public void OpenPanel() {
        anim.Play("addObjectPanel-Up");
        expandButtonText.text = "-";
        mode = PanelMode.Open;
    }

    public void ClosePanel() {
        anim.Play("addObjectPanel-Down");
        expandButtonText.text = "+";
        mode = PanelMode.Close;
    }
}

public enum PanelMode {
    Open, Close
}