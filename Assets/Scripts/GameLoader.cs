using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour {
    public MenuManager menuManager;
    public Slider slider;
    public Text progressText;

    public void LoadGame() {
        menuManager.ChangeMenu(4);
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync() {
        AsyncOperation op = SceneManager.LoadSceneAsync("Game");

        while (!op.isDone) {
            float progress = Mathf.Clamp01(op.progress / 0.9f);
            slider.value = progress;
            progressText.text = (int)(progress * 100) + "%";

            yield return null;
        }
    }
}