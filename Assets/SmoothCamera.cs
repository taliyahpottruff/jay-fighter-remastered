using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {
    private Transform player;

    private void Update() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            return;
        }

        Vector3 newPosition = player.position;
        newPosition.z = -10;
        this.transform.position = newPosition;
    }
}