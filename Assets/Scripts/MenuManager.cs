using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*
    * AUTHOR: Trenton Pottruff
*/

public class MenuManager : MonoBehaviour {
    public GameObject[] menus;

    public void ChangeMenu(int menuIndex) {
        //Loop through the existing menus. Enable the menu that is being changed to and disable all others.
        for (int i = 0; i < menus.Length; i++) {
            if (i == menuIndex) {
                menus[i].SetActive(true);
            } else {
                menus[i].SetActive(false);
            }
        }
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}