using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;

    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchrounously(sceneIndex));
    }

    IEnumerator LoadAsynchrounously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            int progressInt = (int)Math.Round(progress);
            loadingBar.value = progressInt;
            loadingText.text = progressInt * 100 + "%";

            yield return null;
        }
    }
}
