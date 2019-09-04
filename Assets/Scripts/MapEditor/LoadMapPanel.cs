using UnityEngine;
using UnityEngine.UI;
using System.IO;

/*
 * AUTHOR: Trenton Pottruff
*/

public class LoadMapPanel : MonoBehaviour {
    public Transform content;

    public void HidePanel() {
        gameObject.SetActive(false);
    }

	/// <summary>
	/// Show the load map panel.
	/// </summary>
    public void ShowPanel() {
        //Clear the content
        for (int i = 0; i < content.childCount; i++) {
            Destroy(content.GetChild(i).gameObject);
        }

		//Find the path of all the maps
        string[] mapPaths = Directory.GetFiles(Application.persistentDataPath + "/maps");

		//Loop through all paths and instantiate a selectable UI option
        for (int i = 0; i < mapPaths.Length; i++) {
            string mapName = mapPaths[i].Replace(Application.persistentDataPath, "").Replace(".map", "").Replace("/maps\\", "");
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Load Map Option");
            GameObject go = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
            go.name = prefab.name;
            go.transform.SetParent(content);
            Text button = go.GetComponentInChildren<Text>();
            LoadMapOption option = go.GetComponent<LoadMapOption>();
            button.text = mapName;
            option.mapToLoad = mapName;
            option.panel = this;
        }

		//Actually show the panel
        gameObject.SetActive(true);
    }
}
