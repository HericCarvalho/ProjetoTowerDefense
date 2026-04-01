using UnityEngine;

[CreateAssetMenu(menuName = "Skill")]
public class SkillData : ScriptableObject
{
    public string id;
    public string skillName;
    public string description;

    public int starCost;

    public bool unlockedByDefault;

    public float damageBonus;
    public float rangeBonus;
    public float fireRateBonus;
}