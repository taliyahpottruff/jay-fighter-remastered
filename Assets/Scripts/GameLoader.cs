using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * AUTHOR: Trenton Pottruff
 */

public class GameLoader : MonoBehaviour {
    public MenuManager menuManager;
    public Slider slider;
    public Text progressText;

    public void LoadGame() {
        menuManager.ChangeMenu(4);
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync() {
        AsyncOperation op = SceneManager.LoadSceneAsync("Game"); //Begin loading the game and return the operation

        while (!op.isDone) {
            float progress = Mathf.Clamp01(op.progress / 0.9f); //Get a progress float from 0.0 to 1.0
            slider.value = progress;
            progressText.text = (int)(progress * 100) + "%"; //Format the text

            yield return null;
        }
    }
}