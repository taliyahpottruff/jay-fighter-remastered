using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * AUTHOR: Trenton Pottruff
 */

public class MapEditorUI : MonoBehaviour {
    public GameObject selected = null;
    public GameObject ui;
    public EditorMap map;
    public Text selectedObjName;
    public Toggle selectedObjVisibleToggle;
    public Toggle selectedObjColliderToggle;
    public Toggle selectedObjSpawnToggle;
    public InputField xField;
    public InputField yField;

    private MapEditorObject selectedMapObj;
    private bool moveMode;

    private void Update() {
        if (selected != null) {
            ui.SetActive(true);

            selectedMapObj.visible = selectedObjVisibleToggle.isOn;
            selectedMapObj.hasCollider = selectedObjColliderToggle.isOn;
            selectedMapObj.isSpawn = selectedObjSpawnToggle.isOn;

            if (moveMode) {
                Vector2 pos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                //Lock to grid
                if (pos.x > 0)
                    pos.x = (int)(pos.x);
                else if (pos.x < 0)
                    pos.x = (int)(pos.x - 1);

                if (pos.y > 0)
                    pos.y = (int)(pos.y + 1);
                else if (pos.y < 0)
                    pos.y = (int)(pos.y);

                selected.transform.position = pos;

                xField.text = selected.transform.position.x.ToString();
                yField.text = selected.transform.position.y.ToString();
            }
        }
        else {
            ui.SetActive(false);
        }

        if (!EventSystem.current.IsPointerOverGameObject()) {
            if (Input.GetMouseButtonDown(0)) {
                if (!moveMode) {
                    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    if (hit) {
                        if (hit.collider.gameObject.Equals(selected)) {
                            moveMode = true;
                        }
                        else {
                            MapEditorObject obj = hit.collider.gameObject.GetComponent<MapEditorObject>();

                            if (obj != null) {
                                selected = hit.collider.gameObject;

                                selectedObjName.text = selected.name;
                                selectedObjVisibleToggle.isOn = obj.visible;
                                selectedObjColliderToggle.isOn = obj.hasCollider;
                                selectedObjSpawnToggle.isOn = obj.isSpawn;
                                xField.text = obj.transform.position.x.ToString();
                                yField.text = obj.transform.position.y.ToString();

                                selectedMapObj = obj;
                            }
                        }
                    }
                    else {
                        selected = null;
                        selectedMapObj = null;
                    }
                } else {
                    moveMode = false;
                    map.UpdateMap();
                }
            }
        }
    }

    /// <summary>
    /// Sets the selected objects x position
    /// </summary>
    /// <param name="x">The x position to set in the form of a string</param>
    public void SetObjX(string x) {
        if (selected != null) {
            float newVal = 0f;

            if (x != "")
                newVal = float.Parse(x);

            Vector2 position = selected.transform.position;
            position.x = newVal;
            selected.transform.position = position;
            map.UpdateMap();
        }
    }

    /// <summary>
    /// Sets the selected objects y position
    /// </summary>
    /// <param name="y">The y position to set in the form of a string</param>
    public void SetObjY(string y) {
        if (selected != null) {
            float newVal = 0f;

            if (y != "")
                newVal = float.Parse(y);

            Vector2 position = selected.transform.position;
            position.y = newVal;
            selected.transform.position = position;
            map.UpdateMap();
        }
    }

    public void SetObjWidth(string width) {
        if (selected != null) {
            float newVal = 0f;

            if (width != "")
                newVal = float.Parse(width);

            Vector3 scale = selected.transform.localScale;
            scale.x = newVal;
            selected.transform.localScale = scale;
            map.UpdateMap();
        }
    }

    public void SetObjHeight(string height) {
        if (selected != null) {
            float newVal = 0f;

            if (height != "")
                newVal = float.Parse(height);

            Vector3 scale = selected.transform.localScale;
            scale.y = newVal;
            selected.transform.localScale = scale;
            map.UpdateMap();
        }
    }
}