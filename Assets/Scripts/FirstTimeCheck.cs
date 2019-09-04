using UnityEngine;

/*
 * AUTHOR: Trenton Pottruff
 */

public class FirstTimeCheck : MonoBehaviour {
    [SerializeField]
    MenuManager menuManager;

    private void Start() {
		//Is this the player's first time playing...
        if (!PlayerPrefs.HasKey("username"))
            menuManager.ChangeMenu(4);
    }
}