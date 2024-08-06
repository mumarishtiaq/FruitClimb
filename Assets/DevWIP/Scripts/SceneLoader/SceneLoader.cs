using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public TextMeshProUGUI progressText; 
    public GameObject background;
    public CanvasGroup fadeImage;

    void Start()
    {
        StartCoroutine(LoadSceneAsync("GUIScene"));
    }

    public IEnumerator LoadSceneAsync(string sceneName1, string sceneName2 = null)
    {
        
        // Begin to load the scene specified additively
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName1, LoadSceneMode.Additive);

        // Show loading visuals
        SetVisuals(true);

        // While the scene is still loading
        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f) * 100;

            int progressToInt = (int)progress;
            progressText.text = $"Loading + { progressToInt} %" ;

            yield return null;
        }
       
        if (!string.IsNullOrEmpty(sceneName2))
        {
            // Check if the scene is loaded before attempting to unload it
            Scene sceneToUnload = SceneManager.GetSceneByName(sceneName2);
            if (sceneToUnload.isLoaded)
            {
                AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneToUnload);

                while (!unloadOperation.isDone)
                {
                    yield return null;
                }
            }
        }
        yield return fadeImage.DOFade(1, .5f).From(0).WaitForCompletion();

        // Hide loading visuals
        SetVisuals(false);
        yield return fadeImage.DOFade(0, .5f).WaitForCompletion();
    }



    private void SetVisuals(bool isVisible)
    {
        Debug.Log($"Settings visuals {isVisible}");
        progressText.gameObject.SetActive(isVisible);
        background.SetActive(isVisible);
    }

    public void LoadAndUnloadScene(string sceneName, string scenName2 = null)
    {
        StopAllCoroutines();
        StartCoroutine(LoadSceneAsync(sceneName, scenName2));
    }
}
