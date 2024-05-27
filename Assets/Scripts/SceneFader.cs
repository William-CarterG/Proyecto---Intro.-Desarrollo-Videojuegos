using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;  // The UI Image component that will be used for fading
    public float fadeDuration = 1f;  // Duration of the fade effect

    private void Awake()
    {
        // Ensure the fadeImage is fully transparent at the start
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutIn(sceneName));
    }

    private IEnumerator FadeOutIn(string sceneName)
    {
        yield return StartCoroutine(FadeOut());
        yield return StartCoroutine(LoadScene(sceneName));
        yield return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }

    private IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        yield return SceneManager.LoadSceneAsync(sceneName);
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(1f - (elapsedTime / fadeDuration));
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
    }
}