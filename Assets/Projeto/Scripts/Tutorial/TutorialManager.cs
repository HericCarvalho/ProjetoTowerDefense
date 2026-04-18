using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("UI")]
    public CanvasGroup panelGroup;
    public Image darkOverlay;

    public RectTransform highlightCircle;
    public RectTransform arrow;

    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI continueText;

    [Header("Steps")]
    public List<TutorialSteps> steps;

    int currentStep = 0;
    bool isActive = false;
    bool canContinue = false;

    Typewriter typewriter;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Log("Tutorial iniciou");

        if (PlayerPrefs.GetInt("tutorial_done", 0) == 1)
        {
            gameObject.SetActive(false);
            return;
        }

        typewriter = GetComponent<Typewriter>();

        StartCoroutine(StartTutorial());
    }

    void Update()
    {
        if (!isActive || !canContinue) return;

        bool clicked = false;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            clicked = true;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            clicked = true;

        if (clicked)
        {
            canContinue = false;
            NextStep();
        }

        continueText.alpha = Mathf.Abs(Mathf.Sin(Time.unscaledTime * 2f));
    }

    IEnumerator StartTutorial()
    {
        Debug.Log("StartTutorial rodando");

        panelGroup.alpha = 0;
        gameObject.SetActive(true);

        yield return FadeCanvas(panelGroup, 0, 1, 0.3f);

        isActive = true;
        currentStep = 0;

        yield return ShowStep();
    }

    IEnumerator ShowStep()
    {
        canContinue = false;

        if (currentStep >= steps.Count)
        {
            yield return EndTutorial();
            yield break;
        }

        TutorialSteps step = steps[currentStep];

        yield return new WaitForSecondsRealtime(step.delayBefore);

        if (step.target != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(step.target.position);

            highlightCircle.position = pos + step.offset;
            arrow.position = pos + step.offset;
        }

        highlightCircle.gameObject.SetActive(step.useCircle);
        arrow.gameObject.SetActive(step.useArrow);

        tutorialText.text = "";

        yield return StartCoroutine(typewriter.Write(tutorialText, step.text));

        continueText.gameObject.SetActive(true);

        canContinue = true;
    }

    void NextStep()
    {
        continueText.gameObject.SetActive(false);
        currentStep++;
        StartCoroutine(ShowStep());
    }

    IEnumerator EndTutorial()
    {
        canContinue = false;

        yield return FadeCanvas(panelGroup, 1, 0, 0.4f);

        PlayerPrefs.SetInt("tutorial_done", 1);
        PlayerPrefs.Save();

        gameObject.SetActive(false);
    }

    IEnumerator FadeCanvas(CanvasGroup cg, float from, float to, float duration)
    {
        float t = 0;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            cg.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }

        cg.alpha = to;
    }
    public bool IsBlockingInput()
    {
        return isActive;
    }
}