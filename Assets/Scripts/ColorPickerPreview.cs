using UnityEngine;
using UnityEngine.UI;

/*
/* AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Image))]
public class ColorPickerPreview : MonoBehaviour {
    public ColorPickerMode mode;
    public ColorPicker colorPicker;

    private Image image;

    private void Start() {
        image = GetComponent<Image>();

        Color c = Color.white;

		//Set the colour depending on what body part this component is attached to
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
		//Display the appropriate body part with the selected colour
        if (colorPicker.gameObject.activeInHierarchy)
            image.color = colorPicker.selectedColor;
    }
}