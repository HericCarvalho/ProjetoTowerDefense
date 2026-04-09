using System;

[Serializable]
public class SkillModifier
{
    public StatType statType;
    public ModifierType modifierType;
    public TargetType targetType;

    public float value;
}