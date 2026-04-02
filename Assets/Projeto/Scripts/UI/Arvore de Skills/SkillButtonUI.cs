using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillButtonUI : MonoBehaviour
{
    public SkillData skill;

    public Button button;
    public GameObject lockedIcon;
    public GameObject unlockedIcon;
    public Text costText;

    public Image highlight;

    void Start()
    {
        Refresh();
    }
    void Update()
    {
        if (highlight.gameObject.activeSelf)
        {
            float scale = 1f + Mathf.Sin(Time.time * 4f) * 0.05f;
            highlight.transform.localScale = Vector3.one * scale;
        }
    }
    public void Refresh()
    {
        bool unlocked = SkillManager.instance.IsUnlocked(skill.id);
        bool canUnlock = SkillManager.instance.CanUnlock(skill);

        unlockedIcon.SetActive(unlocked);
        lockedIcon.SetActive(!unlocked);

        costText.text = skill.starCost.ToString();

        button.interactable = canUnlock;

        highlight.gameObject.SetActive(canUnlock && !unlocked);
    }

    public void OnClick()
    {
        SkillManager.instance.UnlockSkill(skill);

        PlayUnlockAnimation();

        foreach (var btn in FindObjectsOfType<SkillButtonUI>())
        {
            btn.Refresh();
        }
    }
    public void PlayUnlockAnimation()
    {
        StartCoroutine(UnlockAnim());
    }

    IEnumerator UnlockAnim()
    {
        float t = 0f;
        Vector3 start = transform.localScale;
        Vector3 target = start * 1.2f;

        while (t < 0.2f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(start, target, t / 0.2f);
            yield return null;
        }

        t = 0f;

        while (t < 0.2f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(target, start, t / 0.2f);
            yield return null;
        }
    }
}