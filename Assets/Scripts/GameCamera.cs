using UnityEngine;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

public class GameCamera : MonoBehaviour {
    public Transform player;
    public Transform map;

    private Vector2 mapSize = Vector2.zero;

    private void Update() {
        mapSize = new Vector2(map.localScale.x / 2, map.localScale.y / 2);

        float xRatio = player.position.x / mapSize.x;
        float yRatio = player.position.y / mapSize.y;

        Vector3 cameraVector = new Vector3((mapSize.x / 2) * xRatio, (mapSize.y / 2) * yRatio, -10);
        Camera.main.transform.position = cameraVector;
    }
}
