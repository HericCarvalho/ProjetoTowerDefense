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

    public bool IsUnlocked(string id)
    {
        return unlockedSkills.Contains(id);
    }

    public void UnlockSkill(SkillData skill)
    {
        if (IsUnlocked(skill.id)) return;

        unlockedSkills.Add(skill.id);

        Save();
    }

    public void Save()
    {
        string data = string.Join(",", unlockedSkills);
        PlayerPrefs.SetString("SKILLS", data);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        unlockedSkills.Clear();

        string data = PlayerPrefs.GetString("SKILLS", "");

        if (string.IsNullOrEmpty(data)) return;

        string[] split = data.Split(',');

        foreach (string id in split)
        {
            unlockedSkills.Add(id);
        }
    }
}