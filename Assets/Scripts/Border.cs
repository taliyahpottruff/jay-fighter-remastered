using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

//I'm not even sure this class is used anymore
//TODO Remove this class
public class Border : MonoBehaviour {
    public BorderMode mode = BorderMode.Bottom;

    private void Update() {
        Vector2 worldPos = Vector2.zero;
        Vector2 size = Vector2.one;

        float relativeWidth = (Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)) - Camera.main.ScreenToWorldPoint(Vector2.zero)).x;
        float relativeHeight = (Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)) - Camera.main.ScreenToWorldPoint(Vector2.zero)).y;
    
        switch (mode) {
            case BorderMode.Bottom:
                worldPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, 0)) - new Vector3(0, 0.5f);
                size = new Vector2(relativeWidth, 1);
                break;
            case BorderMode.Top:
                worldPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height)) + new Vector3(0, 0.5f);
                size = new Vector2(relativeWidth, 1);
                break;
            case BorderMode.Left:
                worldPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height / 2)) - new Vector3(0.5f, 0);
                size = new Vector2(1, relativeHeight);
                break;
            case BorderMode.Right:
                worldPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height / 2)) + new Vector3(0.5f, 0);
                size = new Vector2(1, relativeHeight);
                break;
        }

        transform.position = worldPos;
        transform.localScale = size;
    }
}

public enum BorderMode {
    Top, Bottom, Left, Right
}