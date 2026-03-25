using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "TD/Tower Data")]
public class TowerData : ScriptableObject
{
    [Header("Info")]
    public string towerName;
    public GameObject prefab;

    [Header("Base Stats")]
    public float baseRange = 10f;
    public float baseFireRate = 1f;

    [Header("Scaling per Level")]
    public float rangePerLevel = 0.5f;
    public float fireRatePerLevel = 0.1f;

    [Header("XP")]
    public int baseXPToLevel = 10;

    [Header("Upgrade Cost (Restos)")]
    public int baseUpgradeCost = 5;
    public float costMultiplier = 1.5f;

    [Header("Cost")]
    public int costMoney = 50;
    public int costRestos = 0; 

    [Header("Economy")]
    public int upgradeCostMoney;
    public int upgradeCostRestos;
    public int sellValue;

    [Header("Upgrade")]
    public TowerData nextUpgrade;

    [Header("Transmutation")]
    public TowerData transmuteOptionA;
    public TowerData transmuteOptionB;

    [Header("UI")]
    public Sprite icon;
}