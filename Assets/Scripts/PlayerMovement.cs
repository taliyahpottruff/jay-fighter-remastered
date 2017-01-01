using UnityEngine;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    public float speed = 5;
    
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!Game.PAUSED)
            DoUpdate();
        else
            rb.velocity = Vector2.zero;
    }

    private void DoUpdate() {
        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 directionVector = inputVector.normalized;
        rb.velocity = directionVector * speed;
    }
}