using UnityEngine;
using System.Collections.Generic;

public enum SkillType
{
    Passive,
    UnlockModification,
    Active
}

[CreateAssetMenu(menuName = "Skill")]
public class SkillData : ScriptableObject
{
    public string id;
    public string skillName;
    public string description;

    public SkillType type;

    public int starCost;
    public bool unlockedByDefault;

    [Header("Dependencies")]
    public List<SkillData> requiredSkills;

    [Header("Passive Bonuses")]
    public float damageBonus;
    public float rangeBonus;
    public float fireRateBonus;

    [Header("Modification Unlock")]
    public string unlocksModificationID;
}