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

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadRoutine(sceneName));
    }

    IEnumerator LoadRoutine(string sceneName)
    {
        GameObject loading = Instantiate(loadingPrefab);
        DontDestroyOnLoad(loading);

        fadeImage = loading.transform.Find("Canvas/FadeImage").GetComponent<Image>();
        progressBar = loading.transform.Find("Canvas/ProgressBar/Fill").GetComponent<Image>();
        loadingText = loading.transform.Find("Canvas/LoadingText").GetComponent<TextMeshProUGUI>();

        yield return FadeOut();

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
        {
            float progress = Mathf.Clamp01(op.progress / 0.9f);

            progressBar.fillAmount = progress;

            loadingText.text = "Carregando... " + Mathf.RoundToInt(progress * 100f) + "%";

            yield return null;
        }

        progressBar.fillAmount = 1f;
        loadingText.text = "Carregando... 100%";

        yield return new WaitForSecondsRealtime(0.2f);

        op.allowSceneActivation = true;

        yield return FadeIn();

        Destroy(loading);
    }

    IEnumerator FadeOut()
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            c.a = t / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1;
        fadeImage.color = c;
    }

    IEnumerator FadeIn()
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            c.a = 1 - (t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 0;
        fadeImage.color = c;
    }
}