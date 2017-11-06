using UnityEngine;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

public class AddObjectButton : MonoBehaviour {
    public string objectToSpawn;

    /// <summary>
    /// Spawns the object specified by objectToSpawn on the screen. Map Editor function.
    /// </summary>
    public void SpawnObject() {
        //Pre-Spawn
        Vector2 spawnLocation = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width/2, Screen.height/2));
        Transform parent = GameObject.FindGameObjectWithTag("Map").transform;
        spawnLocation = new Vector2((int)spawnLocation.x, (int)spawnLocation.y);
        GameObject prefab = Resources.Load<GameObject>("Prefabs/MapEditorObjects/" + objectToSpawn);

        //Object Spawning
        GameObject go = Instantiate(prefab, spawnLocation, Quaternion.identity) as GameObject;
        go.name = prefab.name;
        go.transform.parent = parent;
    }
}