using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: Trenton Pottruff
/// </summary>

[RequireComponent(typeof(Image))]
public class ColorPickerPreview : MonoBehaviour {
    public ColorPickerMode mode;
    public ColorPicker colorPicker;

    private Image image;

    private void Start() {
        image = GetComponent<Image>();

        Color c = Color.white;

        if (mode == ColorPickerMode.Head) {
            c = new Color(PlayerPrefs.GetFloat("head_r"), PlayerPrefs.GetFloat("head_g"), PlayerPrefs.GetFloat("head_b"));
        }
        else if (mode == ColorPickerMode.Torso) {
            c = new Color(PlayerPrefs.GetFloat("torso_r"), PlayerPrefs.GetFloat("torso_g"), PlayerPrefs.GetFloat("torso_b"));
        }
        else if (mode == ColorPickerMode.Legs) {
            c = new Color(PlayerPrefs.GetFloat("legs_r"), PlayerPrefs.GetFloat("legs_g"), PlayerPrefs.GetFloat("legs_b"));
        }

        image.color = c;
    }

    private void Update() {
        if (colorPicker.gameObject.activeInHierarchy)
            image.color = colorPicker.selectedColor;
    }
}