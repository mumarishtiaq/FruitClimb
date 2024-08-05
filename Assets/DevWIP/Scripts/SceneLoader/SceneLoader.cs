using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public TextMeshProUGUI progressText; 
    public GameObject background; 

    void Start()
    {
        StartCoroutine(LoadSceneAsync("GUIScene"));
    }

    public IEnumerator LoadSceneAsync(string sceneName, string scenName2 = null)
    {
        // Begin to load the scene specified additively
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Show loading visuals
        SetVisuals(true);

        // While the scene is still loading
        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f) * 100;

            // Update the UI Text element with the progress percentage
            progressText.text = "Loading... " + progress.ToString("F2") + "%";

            yield return null;
        }

        // Unloading the old scene, if a scene name is provided
        if (!string.IsNullOrEmpty(scenName2))
        {
            // Check if the scene is loaded before attempting to unload it
            Scene sceneToUnload = SceneManager.GetSceneByName(scenName2);
            if (sceneToUnload.isLoaded)
            {
                AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneToUnload);

                // Wait until the unloading is done
                while (!unloadOperation.isDone)
                {
                    yield return null;
                }
            }
            else
            {
                Debug.LogWarning("Scene " + scenName2 + " is not loaded, so it cannot be unloaded.");
            }
        }

        // Hide loading visuals
        SetVisuals(false);
    }



    private void SetVisuals(bool isVisible)
    {
        progressText.gameObject.SetActive(isVisible);
        background.SetActive(isVisible);
    }
}
