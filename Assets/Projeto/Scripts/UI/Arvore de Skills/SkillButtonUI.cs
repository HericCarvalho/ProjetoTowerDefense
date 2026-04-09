using UnityEngine;
using UnityEngine.UI;

public class SkillButtonUI : MonoBehaviour
{
    public SkillData skill;

    public Button button;
    public Image background;

    public GameObject highlight;

    public Color lockedColor = Color.gray;
    public Color availableColor = Color.white;
    public Color unlockedColor = Color.green;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        bool unlocked = SkillManager.instance.IsUnlocked(skill.id);
        bool canUnlock = SkillManager.instance.CanUnlock(skill);

        if (unlocked)
        {
            button.interactable = false;
            background.color = unlockedColor;

            highlight.SetActive(false);
            return;
        }

        if (canUnlock)
        {
            button.interactable = true;
            background.color = availableColor;

            highlight.SetActive(true);
        }
        else
        {
            button.interactable = false;
            background.color = lockedColor;

            highlight.SetActive(false);
        }
    }

    public void OnClick()
    {
        SkillManager.instance.UnlockSkill(skill);

        foreach (var btn in FindObjectsOfType<SkillButtonUI>())
        {
            btn.Refresh();
        }
    }
}