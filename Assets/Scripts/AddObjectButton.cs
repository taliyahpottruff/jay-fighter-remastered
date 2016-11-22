using UnityEngine;
using System.Collections;

public class AddObjectButton : MonoBehaviour {
    public string objectToSpawn;

    public void SpawnObject() {
        Vector2 spawnLocation = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width/2, Screen.height/2));
        Transform parent = GameObject.FindGameObjectWithTag("Map").transform;
        spawnLocation = new Vector2((int)spawnLocation.x, (int)spawnLocation.y);
        GameObject prefab = Resources.Load<GameObject>("Prefabs/MapEditorObjects/" + objectToSpawn);
        GameObject go = Instantiate(prefab, spawnLocation, Quaternion.identity) as GameObject;
        go.name = prefab.name;
        go.transform.parent = parent;
    }
}