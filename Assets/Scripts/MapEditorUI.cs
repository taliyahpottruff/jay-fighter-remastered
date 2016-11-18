using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MapEditorUI : MonoBehaviour {
    public GameObject selected = null;
    public GameObject ui;
    public Text selectedObjName;
    public Toggle selectedObjVisibleToggle;
    public Toggle selectedObjColliderToggle;
    public InputField widthField;
    public InputField heightField;

    private MapEditorObject selectedMapObj;
    private bool moveMode;

    private void Update() {
        if (selected != null) {
            ui.SetActive(true);

            selectedMapObj.visible = selectedObjVisibleToggle.isOn;
            selectedMapObj.hasCollider = selectedObjColliderToggle.isOn;

            if (moveMode) {
                Vector2 pos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Input.GetKey(KeyCode.Space)) {
                    if (pos.x > 0)
                        pos.x = ((int)pos.x) + 0.5f;
                    else if (pos.x < 0)
                        pos.x = ((int)pos.x) - 0.5f;

                    if (pos.y > 0)
                        pos.y = ((int)pos.y) + 0.5f;
                    else if (pos.y < 0)
                        pos.y = ((int)pos.y) - 0.5f;
                }

                selected.transform.position = pos;
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
                                widthField.text = obj.transform.localScale.x.ToString();
                                heightField.text = obj.transform.localScale.y.ToString();

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
                }
            }
        }
    }

    public void SetObjWidth(string width) {
        if (selected != null) {
            float newWidth = float.Parse(width);
            Vector3 scale = selected.transform.localScale;
            scale.x = newWidth;
            selected.transform.localScale = scale;
        }
    }

    public void SetObjHeight(string height) {
        if (selected != null) {
            float newHeight = float.Parse(height);
            Vector3 scale = selected.transform.localScale;
            scale.y = newHeight;
            selected.transform.localScale = scale;
        }
    }
}