using UnityEngine;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(SpriteRenderer))]
public class MapEditorObject : MonoBehaviour {
    public bool visible = true;
    public bool hasCollider = false;

    private SpriteRenderer sr;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update() {
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