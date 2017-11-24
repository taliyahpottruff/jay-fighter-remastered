using UnityEngine;
using UnityEngine.Networking;

/*
 * AUTHOR: Trenton Pottruff
 */

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : NetworkBehaviour {
    public float speed = 5;
    private float current_speed;
    
    public SpriteRenderer baseRenderer;
    public SpriteRenderer wheelsRenderer;

    [SyncVar]
    public bool base_1 = false;
    [SyncVar]
    public bool side_wheels = false;

    public Sprite[] bases;
    public Sprite[] frontWheels;
    public Sprite[] sideWheels;

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        current_speed = speed;
    }

    private void Update() {
        if (!Game.PAUSED)
            DoUpdate();
        else
            rb.velocity = Vector2.zero;
    }

    private void DoUpdate() {
        if (isLocalPlayer) {
            Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 directionVector = inputVector.normalized;

            float horizontal = Mathf.Abs(directionVector.x);
            float vertical = Mathf.Abs(directionVector.y);

            if (horizontal != 0 || vertical != 0) {
                if (horizontal > vertical) {
                    base_1 = true;
                    side_wheels = true;
                }
                else {
                    base_1 = false;
                    side_wheels = false;
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

    public void SetSpeed(float multiplier) {
        current_speed = speed * multiplier;
    }
}