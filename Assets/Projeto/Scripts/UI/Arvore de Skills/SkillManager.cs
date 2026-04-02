using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public List<SkillData> allSkills;

    private HashSet<string> unlockedSkills = new HashSet<string>();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public bool CanUnlock(SkillData skill)
    {
        if (IsUnlocked(skill.id)) return false;

        bool hasAdjacentUnlocked = skill.requiredSkills.Count == 0;

        foreach (var req in skill.requiredSkills)
        {
            if (IsUnlocked(req.id))
            {
                hasAdjacentUnlocked = true;
                break;
            }
        }

        if (!hasAdjacentUnlocked)
            return false;

        if (!PlayerStars.instance.CanSpend(skill.starCost))
            return false;

        return true;
    }

    public void UnlockSkill(SkillData skill)
    {
        if (!CanUnlock(skill)) return;

        PlayerStars.instance.Spend(skill.starCost);

        unlockedSkills.Add(skill.id);

        Save();
    }
    public bool IsUnlocked(string id)
    {
        return unlockedSkills.Contains(id);
    }

    public float GetDamageMultiplier()
    {
        float total = 1f;

        foreach (var skill in allSkills)
        {
            if (IsUnlocked(skill.id))
            {
                total += skill.damageBonus;
            }
        }

        return total;
    }
    public bool IsModificationUnlocked(string modID)
    {
        foreach (var skill in allSkills)
        {
            if (IsUnlocked(skill.id) && skill.unlocksModificationID == modID)
                return true;
        }

        return false;
    }

    public void Save()
    {
        string data = string.Join(",", unlockedSkills);
        PlayerPrefs.SetString(SaveContext.GetKey("SKILLS"), data);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        unlockedSkills.Clear();

        string data = PlayerPrefs.GetString(SaveContext.GetKey("SKILLS"), "");

        if (string.IsNullOrEmpty(data)) return;

        string[] split = data.Split(',');

        foreach (string id in split)
        {
            unlockedSkills.Add(id);
        }
    }
}