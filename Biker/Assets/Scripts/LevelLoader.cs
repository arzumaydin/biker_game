using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScene;
    public Slider slider;

    private void Start() {
        LoadScene(1);
    }
    private void LoadScene(int sceneIndex) {
        StartCoroutine(LoadMainScene(sceneIndex));
    }

    IEnumerator LoadMainScene(int sceneIndex) {

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneIndex);
        yield return 0;
        loadingScene.SetActive(true);

        while(!op.isDone) {

            float progress = Mathf.Clamp01(op.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
