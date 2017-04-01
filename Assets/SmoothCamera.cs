using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {
    public Transform lookAt;

    private float smoothSpeed = 5f;
    private Vector3 offset = new Vector3(0, 0, -10);

    public void FixedUpdate() {
        try {
            if (lookAt == null) {
                Debug.Log("lookAt is null!");
                lookAt = GameObject.FindGameObjectWithTag("Player").transform;
                return;
            }

            Vector3 newPosition = lookAt.position + offset;

            //Smooth
            transform.position -= (transform.position - newPosition) * smoothSpeed * Time.deltaTime;
        } catch (Exception e) { }
    }
}