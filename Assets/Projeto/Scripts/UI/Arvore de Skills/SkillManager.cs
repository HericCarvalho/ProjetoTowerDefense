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

    public bool IsModificationUnlocked(string modID)
    {
        foreach (var skill in allSkills)
        {
            if (IsUnlocked(skill.id) && skill.unlocksModificationID == modID)
                return true;
        }

        return false;
    }
    public float GetStat(StatType stat, TargetType target, float baseValue)
    {
        float flat = 0f;
        float percent = 0f;

        foreach (var skill in allSkills)
        {
            if (!IsUnlocked(skill.id)) continue;

            foreach (var mod in skill.modifiers)
            {
                if (mod.statType != stat) continue;

                if (!Affects(mod.targetType, target))
                    continue;

                if (mod.modifierType == ModifierType.Flat)
                    flat += mod.value;

                if (mod.modifierType == ModifierType.Percent)
                    percent += mod.value;
            }
        }

        return (baseValue + flat) * (1f + percent);
    }
    public bool Affects(TargetType skillTarget, TargetType target)
    {
        if (skillTarget == TargetType.AllTowers &&
            (target == TargetType.Ballista ||
             target == TargetType.Magic ||
             target == TargetType.Cannon ||
             target == TargetType.Earthquake))
            return true;

        return skillTarget == target;
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