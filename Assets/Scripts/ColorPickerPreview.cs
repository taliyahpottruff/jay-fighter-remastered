using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: Trenton Pottruff
/// </summary>

[RequireComponent(typeof(Image))]
public class ColorPickerPreview : MonoBehaviour {
    public ColorPicker colorPicker;

    private Image image;

    private void Start() {
        image = GetComponent<Image>();
    }

    private void Update() {
        image.color = colorPicker.selectedColor;
    }
}