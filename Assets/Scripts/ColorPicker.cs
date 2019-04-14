using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: Trenton Pottruff
/// </summary>

public class ColorPicker : MonoBehaviour {
    public ColorPickerMode mode;
    public Color selectedColor;

    private Rect spectrumRect;
    private float size;

    string suffix;

    private void Start() {
        spectrumRect = this.transform.parent.GetComponent<RectTransform>().rect;
        size = Mathf.Min(spectrumRect.width, spectrumRect.height);

        if (!PlayerPrefs.HasKey("cPicker_xh") || !PlayerPrefs.HasKey("cPicker_yh")) {
            PlayerPrefs.SetFloat("cPicker_xh", -(size/2f));
            PlayerPrefs.SetFloat("cPicker_yh", (size/2f));
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("cPicker_xt") || !PlayerPrefs.HasKey("cPicker_yt")) {
            PlayerPrefs.SetFloat("cPicker_xt", -(size / 2f));
            PlayerPrefs.SetFloat("cPicker_yt", (size / 2f));
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("cPicker_xl") || !PlayerPrefs.HasKey("cPicker_yl")) {
            PlayerPrefs.SetFloat("cPicker_xl", -(size / 2f));
            PlayerPrefs.SetFloat("cPicker_yl", (size / 2f));
            PlayerPrefs.Save();
        }

        Image parentImage = this.transform.parent.GetComponent<Image>();
        suffix = "h";

        if (mode == ColorPickerMode.Torso)
            suffix = "t";
        else if (mode == ColorPickerMode.Legs)
            suffix = "l";

        float x = PlayerPrefs.GetFloat("cPicker_x" + suffix);
        float y = PlayerPrefs.GetFloat("cPicker_y" + suffix);
        Vector2 startingPos = new Vector2(x, y);
        this.transform.localPosition = startingPos;
        x += (size/2f);
        y += (size/2f);
        selectedColor = parentImage.sprite.texture.GetPixel((int)((x / size) * 1080f), (int)((y / size) * 1080f));
    }

    public void DragMeAway(BaseEventData eventData) {
        spectrumRect = this.transform.parent.GetComponent<RectTransform>().rect;
        size = Mathf.Min(spectrumRect.width, spectrumRect.height);

        PointerEventData pt = (PointerEventData) eventData;
        Vector2 pos = pt.position;
        pos.x = Mathf.Clamp(pos.x, this.transform.parent.position.x - (size/2f), this.transform.parent.position.x + (size/2f));
        pos.y = Mathf.Clamp(pos.y, this.transform.parent.position.y - (size/2f), this.transform.parent.position.y + (size/2f));
        this.transform.position = pos;

        RectTransform rt = GetComponent<RectTransform>();
        float x = this.transform.localPosition.x + (size/2f);
        float y = this.transform.localPosition.y + (size/2f);

        Image parentImage = this.transform.parent.GetComponent<Image>();
        selectedColor = parentImage.sprite.texture.GetPixel((int)((x/size)*1080f), (int)((y/size)*1080f));

        if (mode == ColorPickerMode.Head) {
            PlayerPrefs.SetFloat("head_r", selectedColor.r);
            PlayerPrefs.SetFloat("head_g", selectedColor.g);
            PlayerPrefs.SetFloat("head_b", selectedColor.b);
        } else if (mode == ColorPickerMode.Torso) {
            PlayerPrefs.SetFloat("torso_r", selectedColor.r);
            PlayerPrefs.SetFloat("torso_g", selectedColor.g);
            PlayerPrefs.SetFloat("torso_b", selectedColor.b);
        } else if (mode == ColorPickerMode.Legs) {
            PlayerPrefs.SetFloat("legs_r", selectedColor.r);
            PlayerPrefs.SetFloat("legs_g", selectedColor.g);
            PlayerPrefs.SetFloat("legs_b", selectedColor.b);
        }

        PlayerPrefs.SetFloat("cPicker_x" + suffix, this.transform.localPosition.x);
        PlayerPrefs.SetFloat("cPicker_y" + suffix, this.transform.localPosition.y);
        PlayerPrefs.Save();
    }
}

public enum ColorPickerMode {
    Head, Torso, Legs
}