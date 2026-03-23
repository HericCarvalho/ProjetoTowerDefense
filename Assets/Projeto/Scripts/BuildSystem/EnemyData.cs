using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "TD/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyID;
    public string enemyName;
    public Sprite icon;

    [Header("Stats")]
    public float maxHealth;
    public float damage;
    public float speed;
    public float attackSpeed;
    public float attackRange;

    [Header("Resistances")]
    [Range(0, 1)] public float physicalResistance;
    [Range(0, 1)] public float magicalResistance;

    [Header("Immunities")]
    public bool immuneToBurn;
    public bool immuneToSlow;
    public bool immuneToStun;

    [TextArea]
    public string description;
}