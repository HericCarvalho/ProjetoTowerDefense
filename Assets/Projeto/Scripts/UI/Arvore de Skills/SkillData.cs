using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill")]
public class SkillData : ScriptableObject
{
    public string id;
    public string skillName;
    public string description;

    public int starCost;

    public bool unlockedByDefault;

    public List<SkillData> requiredSkills;

    public List<SkillModifier> modifiers;

    public string unlocksModificationID;
}