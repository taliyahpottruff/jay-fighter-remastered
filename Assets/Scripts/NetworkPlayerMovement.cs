using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

/*
    * AUTHOR: Trenton Pottruff
*/

[RequireComponent(typeof(Rigidbody2D))]
public class NetworkPlayerMovement : NetworkBehaviour {
    public float speed = 5;

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!isLocalPlayer)
            return;

        DoUpdate();
    }

    private void DoUpdate() {
        Vector2 inputVector = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        Vector2 directionVector = inputVector.normalized;
        rb.velocity = directionVector * speed;
    }
}