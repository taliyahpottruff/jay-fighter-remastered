using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class MapSelectionDisplay : MonoBehaviour {
    public MenuManager menuManager;

    public GameObject mapSelectButtonPrefab;

    private void Start() {
        PopulateList();
    }

    private void PopulateList() {
        Map[] maps = new Map[Game.MAPS.Count];
        Game.MAPS.Values.CopyTo(maps, 0);
        string[] mapPaths = new string[] { };

        if (Directory.Exists(Application.persistentDataPath + "/maps"))
            mapPaths = Directory.GetFiles(Application.persistentDataPath + "/maps");

        string[] names = new string[maps.Length + mapPaths.Length];

        for (int i = 0; i < maps.Length; i++) {
            names[i] = maps[i].name;
        }
        for (int i = 0; i < mapPaths.Length; i++) {
            names[i + maps.Length] = mapPaths[i].Replace(Application.persistentDataPath, "").Replace(".map", "").Replace("/maps\\", "");
        }

    

        Utilities.ClearChildren(this.transform);
        for (int i = 0; i < names.Length; i++) {
            GameObject go = Instantiate(mapSelectButtonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            go.name = mapSelectButtonPrefab.name;
            go.transform.SetParent(this.transform);
            MapSelectButton msb = go.GetComponent<MapSelectButton>();
            
            msb.SetInfo(names[i], "This is a map!");
        }
    }

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    public void BackToMain() {
        menuManager.ChangeMenu(0);
    }
}