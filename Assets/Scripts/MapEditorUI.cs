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
                            Debug.Log(hit.collider.gameObject.name);

                            if (obj != null) {
                                selected = hit.collider.gameObject;

                                selectedObjName.text = selected.name;
                                selectedObjVisibleToggle.isOn = obj.visible;
                                selectedObjColliderToggle.isOn = obj.hasCollider;

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
}