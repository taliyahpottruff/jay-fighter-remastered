﻿using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {
    public float speed = 5;
    private float current_speed;
    
    public SpriteRenderer baseRenderer;
    public SpriteRenderer wheelsRenderer;

    public bool base_1 = false;
    public bool side_wheels = false;

    public Sprite[] bases;
    public Sprite[] frontWheels;
    public Sprite[] sideWheels;

    private Rigidbody2D rb;
    private Player player;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        current_speed = speed;
    }

    private void Update() {
        if (!Game.PAUSED)
            DoUpdate();
        else
            rb.velocity = Vector2.zero;
    }

	//Acts as a abstraction
    private void DoUpdate() {
		//If this player is the local player then it can be controlled
        if (player.isLocalPlayer) {
            Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 directionVector = inputVector.normalized;

            float horizontal = Mathf.Abs(directionVector.x);
            float vertical = Mathf.Abs(directionVector.y);

            if (horizontal != 0 || vertical != 0) {
                if (horizontal > vertical) {
                    CmdSetBottom(true, true);
                }
                else {
                    CmdSetBottom(false, false);
                }
            }

            rb.velocity = directionVector * current_speed;
        }

        if (base_1) {
            baseRenderer.sprite = bases[1];
        } else {
            baseRenderer.sprite = bases[0];
        }

        if (side_wheels) {
            wheelsRenderer.sprite = sideWheels[0];
        } else {
            wheelsRenderer.sprite = frontWheels[0];
        }
    }

    public void CmdSetBottom(bool base_1, bool side_wheels) {
        this.base_1 = base_1;
        this.side_wheels = side_wheels;
    }

    public void SetSpeed(float multiplier) {
        current_speed = speed * multiplier;
    }
}