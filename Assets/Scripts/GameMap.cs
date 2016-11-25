using UnityEngine;
using System.Collections;
using System.IO;

public class GameMap : MonoBehaviour {
    private Map map;

    private void Start() {
        Utilities.ClearChildren(this.transform);
        map = Game.LoadCurrentMap();
    }

    public void LoadMap(string mapToLoad) {
        //To Change
        FileStream fs = new FileStream(directory + "/" + mapToLoad + ".map", FileMode.Open);
        using (StreamReader reader = new StreamReader(fs)) {
            string json = Utilities.Base64Decode(reader.ReadToEnd());
            map = JsonUtility.FromJson<Map>(json);
            GameObject mapGO = GameObject.FindGameObjectWithTag("Map");
            //Clears the map object of existing children
            for (int i = 0; i < mapGO.transform.childCount; i++) {
                Destroy(mapGO.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < map.objects.Length; i++) {
                MapObj mapObj = map.objects[i];
                GameObject prefab = Resources.Load<GameObject>("Prefabs/MapObjects/" + mapObj.name);
                GameObject newGO = Instantiate(prefab, new Vector2(mapObj.x, mapObj.y), Quaternion.identity, mapGO.transform) as GameObject;
                newGO.name = prefab.name;
                Vector3 size = Vector3.one;
                size.x = mapObj.width;
                size.y = mapObj.height;
                newGO.transform.localScale = size;
                MapEditorObject newMapEditObj = newGO.GetComponent<MapEditorObject>();
                newMapEditObj.visible = mapObj.visible;
                newMapEditObj.hasCollider = mapObj.collider;
            }
        }
    }
}
