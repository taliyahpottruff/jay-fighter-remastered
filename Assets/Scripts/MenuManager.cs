using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * AUTHOR: Trenton Pottruff
 */

public class MenuManager : MonoBehaviour {
    public GameObject[] menus;

    /// <summary>
    /// Changes the menu
    /// </summary>
    /// <param name="menuIndex">The ID of the menu to be changed to</param>
    public void ChangeMenu(int menuIndex) {
        //Loop through the existing menus. Enable the menu that is being changed to and disable all others.
        for (int i = 0; i < menus.Length; i++) {
            if (i == menuIndex) {
				if (menus[i] != null) menus[i].SetActive(true);
            } else {
				if (menus[i] != null) menus[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void ExitGame() {
        Application.Quit();
    }

    /// <summary>
    /// Loads a scene
    /// </summary>
    /// <param name="sceneName">The scene to be loaded</param>
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}