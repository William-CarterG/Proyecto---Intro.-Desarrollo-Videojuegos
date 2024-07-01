using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTextScript : MonoBehaviour
{
    public float fadeDuration = 2f; // Duration of the fade effect
    private CanvasGroup canvasGroup;
    private bool firstTime;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        // Check if this is the first time the game is run
        firstTime = PlayerPrefs.GetInt("FirstTimetext", 1) == 1;

        if (firstTime)
        {
            // If it's the first time, show the text and start the fade out coroutine
            canvasGroup.alpha = 1f;
            StartCoroutine(FadeOutText());

            // Set the flag to indicate that the game has been run before
            PlayerPrefs.SetInt("FirstTimetext", 0);
            PlayerPrefs.Save();
        }
        else
        {
            // If it's not the first time, hide the text immediately
            canvasGroup.alpha = 0f;
        }
    }

    IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }
}