using UnityEngine;

public class TowerStats : MonoBehaviour
{
    public TowerData data;

    [Header("Runtime Stats")]
    public float range;
    public float fireRate;
    public float damage;

    [Header("Damage Type")]
    public bool isMagicDamage;
    public bool isTrueDamage;

    [Header("Prefabs")]
    public GameObject bulletPrefab;

    void Awake()
    {
        ApplyData();
    }

    public void ApplyData()
    {
        if (data == null)
            return;

        range = data.range;
        fireRate = data.fireRate;
        damage = data.damage;
    }
}