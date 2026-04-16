using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class LoadingScreenController : MonoBehaviour
{
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        float minLoadTime = 1f;
        float timer = 0f;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            timer += Time.deltaTime;

            float realProgress = Mathf.Clamp01(operation.progress / 0.9f);
            float timeProgress = Mathf.Clamp01(timer / minLoadTime);
            float targetProgress = Mathf.Min(realProgress, timeProgress);
            loadingBar.value = Mathf.Lerp(loadingBar.value, targetProgress, Time.deltaTime * 8f);
            
            if (loadingText != null)
            {
                loadingText.text = "LOADING... " + Mathf.Round(loadingBar.value * 100f) + "%";
            }

            if (operation.progress >= 0.9f && timer >= minLoadTime)
            {
                loadingBar.value = 1.0f;
                if (loadingText != null)
                {
                    loadingText.text = "LOADING... 100%";
                }
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
