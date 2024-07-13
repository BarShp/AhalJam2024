using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FadeText : MonoBehaviour
{
    public TextMeshProUGUI textToFade;
    public float fadeDuration = 3f;

    void Start()
    {
        if (textToFade != null)
        {
            StartCoroutine(FadeOutText(fadeDuration, textToFade));
        }
        else
        {
            Debug.LogError("Text component is not assigned.");
        }
    }

    IEnumerator FadeOutText(float fadeDuration, TextMeshProUGUI text)
    {
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeDuration));
            yield return null;
        }
        text.color = Color.clear;
    }
}