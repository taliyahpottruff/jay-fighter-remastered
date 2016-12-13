using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
    public GameObject[] menus;

    public void ChangeMenu(int menuIndex) {
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
}