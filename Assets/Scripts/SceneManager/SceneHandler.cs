using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    public int LoadSceneIndex;

    private void Start()
    {
        fader.gameObject.SetActive(true);

        LeanTween.alpha (fader, 1, 0);
        LeanTween.alpha (fader, 0, 0.5f).setOnComplete (() => {
            fader.gameObject.SetActive (false);
        });

    }
    public void OpenMenuScene()
    {
        fader.gameObject.SetActive(true);

        LeanTween.alpha (fader, 0, 0);
        LeanTween.alpha (fader, 1, 0.5f).setOnComplete (() => {
            SceneManager.LoadScene (LoadSceneIndex);
        });

    }

    public void OpenGameScene()
    {
        fader.gameObject.SetActive(true);

        // ALPHA
        LeanTween.alpha (fader, 0, 0);
        LeanTween.alpha (fader, 1, 0.5f).setOnComplete (() => {
        //     // Example for little pause before laoding the next scene
         Invoke ("LoadGame", 0.5f);
        });
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(LoadSceneIndex);
    }
}
