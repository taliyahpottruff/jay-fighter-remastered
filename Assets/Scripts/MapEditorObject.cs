using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(SpriteRenderer))]
public class MapEditorObject : MonoBehaviour {
    public bool visible = true;
    public bool hasCollider = false;
    public bool isSpawn = false;

    private SpriteRenderer sr;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        //If the object is a spawn point, it cannot have a collider or be visible
        if (isSpawn) {
            hasCollider = false;
            visible = false;
        }

        if (visible) {
            Color c = sr.color;
            c.a = 1f;
            sr.color = c;
        } else {
            Color c = sr.color;
            c.a = 0.5f;
            sr.color = c;
        }
    }
}