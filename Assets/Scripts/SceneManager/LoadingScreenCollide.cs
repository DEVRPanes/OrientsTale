using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreenCollide : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;
    public int sceneIndex;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchrounously(sceneIndex));
    }

    IEnumerator LoadAsynchrounously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;
            loadingText.text = progress * 100 + "%";

            yield return null;
        }
    }
}
