using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// AUTHOR: Trenton Pottruff
/// </summary>

public class ColorPicker : MonoBehaviour {
    public Color selectedColor;

    private void Start() {
        Image parentImage = this.transform.parent.GetComponent<Image>();
        selectedColor = parentImage.sprite.texture.GetPixel(0, 1080);
    }

    public void DragMeAway(BaseEventData eventData) {
        PointerEventData pt = (PointerEventData) eventData;
        Vector2 pos = pt.position;
        pos.x = Mathf.Clamp(pos.x, this.transform.parent.position.x - 150f, this.transform.parent.position.x + 150f);
        pos.y = Mathf.Clamp(pos.y, this.transform.parent.position.y - 150f, this.transform.parent.position.y + 150f);
        this.transform.position = pos;

        RectTransform rt = GetComponent<RectTransform>();
        float x = this.transform.localPosition.x + 150;
        float y = this.transform.localPosition.y + 150;

        Image parentImage = this.transform.parent.GetComponent<Image>();
        selectedColor = parentImage.sprite.texture.GetPixel((int)((x/300f)*1080f), (int)((y/300f)*1080f));
    }
}