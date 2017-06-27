using UnityEngine;
using System.Collections;
using System.IO;

public class GameMap : MonoBehaviour {
    public Vector2 size;

    private Map map;
    
    private void Awake() {
        Utilities.ClearChildren(this.transform);
        map = Game.LoadCurrentMap();
        LoadMap();
    }

    public void LoadMap() {
        size = new Vector2(map.width, map.height);
        GameObject mapGO = GameObject.FindGameObjectWithTag("Map");
        //Clears the map object of existing children
        Utilities.ClearChildren(mapGO.transform);
        Debug.Log("There are " + map.objects.Length + " objects in the new map!");
        for (int i = 0; i < map.objects.Length; i++) {
            MapObj mapObj = map.objects[i];
            GameObject prefab = Resources.Load<GameObject>("Prefabs/MapObjects/" + mapObj.name);
            GameObject newGO = Instantiate(prefab, new Vector2(mapObj.x, mapObj.y), Quaternion.identity) as GameObject;

            Vector3 size = Vector3.one;
            size.x = mapObj.width;
            size.y = mapObj.height;
            newGO.transform.localScale = size;

            newGO.name = prefab.name;
            newGO.transform.SetParent(mapGO.transform);
            
            MapObject newMapObj = newGO.GetComponent<MapObject>();
            newMapObj.visible = mapObj.visible;
            newMapObj.hasCollider = mapObj.collider;
            newMapObj.isSpawn = mapObj.spawn;
        }
        
    }
}
