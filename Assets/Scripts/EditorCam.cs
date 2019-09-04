using UnityEngine;

/*
 * AUTHOR: Garrett Nicholas
 */

public class EditorCam : MonoBehaviour {
    public Camera cam;
    private Transform rig;
    private Vector3 lastPos;
    private int moveSpeed = 100;
    public float zoomSpeed = 4;
    public float targetOrtho;
    public float smoothSpeed = 8.0f;
    public float minOrtho = 1.0f;
    public float maxOrtho = 15.0f;
    public bool canMove = true;

    void Start() {
        if (cam == null) {
            cam = GetComponent<Camera>();
        }
        if (cam == null) {
            cam = Camera.main;
        }
        if (cam == null) {
            Debug.LogError("No Camera!");
        }
        rig = cam.transform.parent;
        targetOrtho = cam.orthographicSize;
    }

    void Update() {
        if (canMove) {
            mouseMovement();
            camZoom();
            updateCamMoveSpeed();
        }
    }

    void updateCamMoveSpeed() {
        if (targetOrtho < 2.5) {
            moveSpeed = 150;
        }
        else if (targetOrtho >= 2.5 && targetOrtho <= 10) {
            moveSpeed = 100;
        }
        else if (targetOrtho >= 10 && targetOrtho <= 13) {
            moveSpeed = 50;
        }
        else if (targetOrtho >= 13 && targetOrtho <= 15) {
            moveSpeed = 25;
        }
    }

    void camZoom() {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f) {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
    }

    void mouseMovement() {
        if (Input.GetMouseButtonDown(1) == true) {
            lastPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(1) == true) {
            Vector3 mPos = Input.mousePosition;
            Vector3 move = mPos - lastPos;
            rig.position = rig.position - move / moveSpeed;
            lastPos = mPos;
        }
    }
}