using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public class FirstTimeCheck : MonoBehaviour {
    [SerializeField]
    MenuManager menuManager;

    private void Start() {
        if (!PlayerPrefs.HasKey("username"))
            menuManager.ChangeMenu(4);
    }
}