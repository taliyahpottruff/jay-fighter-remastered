using UnityEngine;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class MapObject : MonoBehaviour {
    public bool visible = true;
    public bool hasCollider = false;
    public bool isSpawn = false;

    private SpriteRenderer sr;
    private Collider2D col;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        if (transform.localScale.x > 1) {
            Vector2 off = col.offset;
            off.x = 0.455f;
            col.offset = off;
        }

        if (transform.localScale.y > 1) {
            Vector2 off = col.offset;
            off.y = -0.455f;
            col.offset = off;
        }
    }

    private void Update() {
        //If the object is a spawn point, it cannot have a collider or be visible
        if (isSpawn) {
            hasCollider = false;
            visible = false;

            //Set the appropriate tags
            if (gameObject.tag != "Spawn Point")
                gameObject.tag = "Spawn Point";
        }

        //Set Visibility
        if (visible) {
            Color c = sr.color;
            c.a = 1f;
            sr.color = c;
        }
        else {
            Color c = sr.color;
            c.a = 0f;
            sr.color = c;
        }

        //Set Collider
        if (hasCollider) {
            col.enabled = true;
        } else {
            col.enabled = false;
        }
    }
}