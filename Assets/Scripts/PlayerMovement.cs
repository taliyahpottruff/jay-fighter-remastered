using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : NetworkBehaviour {
    public float speed = 5;
    
    public SpriteRenderer baseRenderer;
    public SpriteRenderer wheelsRenderer;

    public Sprite[] bases;
    public Sprite[] frontWheels;
    public Sprite[] sideWheels;

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

        float horizontal = Mathf.Abs(directionVector.x);
        float vertical = Mathf.Abs(directionVector.y);

        if (horizontal != 0 || vertical != 0) {
            if (horizontal > vertical) {
                baseRenderer.sprite = bases[1];
                wheelsRenderer.sprite = sideWheels[0];
            } else {
                baseRenderer.sprite = bases[0];
                wheelsRenderer.sprite = frontWheels[0];
            }
        }

        rb.velocity = directionVector * speed;
    }
}