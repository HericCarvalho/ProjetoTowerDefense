using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public GameObject loadingPrefab;

    Image fadeImage;
    Image progressBar;
    TextMeshProUGUI loadingText;

    CanvasGroup contentGroup;

    public float fadeDuration = 0.5f;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName, CanvasGroup hudGroup = null)
    {
        StartCoroutine(LoadRoutine(sceneName, hudGroup));
    }

    IEnumerator LoadRoutine(string sceneName, CanvasGroup hudGroup = null)
    {
        GameObject loading = Instantiate(loadingPrefab);
        DontDestroyOnLoad(loading);

        Transform canvas = loading.transform.Find("Canvas");

        fadeImage = canvas.Find("FadeImage").GetComponent<Image>();
        progressBar = canvas.Find("Content/ProgressBar/Fill").GetComponent<Image>();
        loadingText = canvas.Find("Content/LoadingText").GetComponent<TextMeshProUGUI>();
        CanvasGroup contentGroup = canvas.Find("Content").GetComponent<CanvasGroup>();

        if (contentGroup == null)
            contentGroup = canvas.Find("Content").gameObject.AddComponent<CanvasGroup>();

        contentGroup.alpha = 0f;

        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;

        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.SmoothStep(0, 1, t / fadeDuration);

            if (hudGroup != null)
                hudGroup.alpha = 1 - alpha;

            c.a = alpha;
            fadeImage.color = c;

            yield return null;
        }

        c.a = 1f;
        fadeImage.color = c;

        if (hudGroup != null)
            hudGroup.alpha = 0;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        contentGroup.alpha = 1f;

        while (op.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);

            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, progress, 10f * Time.unscaledDeltaTime);
            loadingText.text = "Carregando... " + Mathf.RoundToInt(progress * 100f) + "%";

            yield return null;
        }

        progressBar.fillAmount = 1f;
        loadingText.text = "Carregando... 100%";

        yield return new WaitForSecondsRealtime(0.2f);

        op.allowSceneActivation = true;

        while (!op.isDone)
            yield return null;

        c.a = 1f;
        fadeImage.color = c;

        contentGroup.alpha = 1f;

        yield return null;

        t = 0;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = 1 - Mathf.SmoothStep(0, 1, t / fadeDuration);

            c.a = alpha;
            fadeImage.color = c;

            contentGroup.alpha = alpha;

            yield return null;
        }

        c.a = 0f;
        fadeImage.color = c;

        Destroy(loading);
    }

    IEnumerator FadeOut(CanvasGroup hud, CanvasGroup loading)
    {
        float t = 0;
        Color c = fadeImage.color;

        c.a = 0;
        fadeImage.color = c;

        loading.alpha = 0;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.SmoothStep(0, 1, t / fadeDuration);

            if (hud != null)
                hud.alpha = 1 - alpha;

            c.a = alpha;
            fadeImage.color = c;

            yield return null;
        }

        c.a = 1;
        fadeImage.color = c;

        if (hud != null)
            hud.alpha = 0;

        yield return new WaitForSecondsRealtime(0.1f);

        t = 0;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = Mathf.SmoothStep(0, 1, t / fadeDuration);

            loading.alpha = alpha;

            yield return null;
        }

        loading.alpha = 1;
    }

    IEnumerator FadeIn(CanvasGroup contentGroup)
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            float alpha = 1 - (t / fadeDuration);

            c.a = alpha;
            fadeImage.color = c;

            contentGroup.alpha = alpha;

            yield return null;
        }

        c.a = 0;
        fadeImage.color = c;
        contentGroup.alpha = 0;
    }

    public void LoadSceneByIndex(int index, CanvasGroup hudGroup = null)
    {
        string sceneName = SceneManager.GetSceneByBuildIndex(index).name;
        LoadScene(sceneName, hudGroup);
    }
}