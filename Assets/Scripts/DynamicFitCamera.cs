using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DynamicFitCamera : MonoBehaviour {
    private List<GameObject> entities = new List<GameObject>();
    private Vector2 min = Vector2.zero;
    private Vector2 max = Vector2.one;
    private float viewWidth = 1f;
    private float viewHeight = 1f;
    private Vector2 meanVector = Vector2.zero;

    private void Update() {
        entities.Clear();
        min = Vector2.zero;
        max = Vector2.zero;

        entities.AddRange(GameObject.FindGameObjectsWithTag("Entity"));
        entities.Add(GameObject.FindGameObjectWithTag("Player"));

        meanVector = Vector2.zero;

        //Loop through the entities and grab the min and max values
        for (int i = 0; i < entities.Count; i++) {
            Vector2 pos = entities[i].transform.position;

            if (pos.x < min.x)
                min.x = pos.x;
            if (pos.y < min.y)
                min.y = pos.y;
            if (pos.x > max.x)
                max.x = pos.x;
            if (pos.y > max.y)
                max.y = pos.y;

            meanVector += pos;
        }

        //Set the view width and height
        viewWidth = max.x - min.x;
        viewHeight = max.y - min.y;

        //commented out because of console spam
        //Debug.Log(viewHeight);

        meanVector /= entities.Count;
        transform.position = new Vector3(meanVector.x, meanVector.y, -10);
        Camera.main.orthographicSize = 5 + (viewHeight / 2);
    }
}
