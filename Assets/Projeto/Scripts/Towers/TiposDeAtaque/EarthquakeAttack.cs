using UnityEngine;

public class EarthquakeAttack : MonoBehaviour
{
    [Header("Stats")]
    public float damage = 50f;
    public float radius = 5f;

    [Header("Damage Type")]
    public bool isMagicDamage;
    public bool isTrueDamage;

    [Header("Effects Chance")]
    public float burnChance;
    public float slowChance;
    public float stunChance;

    [Header("Effects Power")]
    public float burnDuration;
    public float burnDPS;

    public float slowDuration;
    public float slowMultiplier;

    public float stunDuration;

    [Header("Visual")]
    public GameObject effectPrefab;

    public void Execute(Vector3 position)
    {
        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, position, Quaternion.identity);
        }

        Collider[] hits = Physics.OverlapSphere(position, radius);

        foreach (Collider col in hits)
        {
            EnemyHealth enemy = col.GetComponent<EnemyHealth>();

            if (enemy == null)
                continue;

            enemy.TakeDamage(damage, isMagicDamage, isTrueDamage);

            if (Random.value <= burnChance)
                enemy.ApplyBurn(burnDuration, burnDPS);

            if (Random.value <= slowChance)
                enemy.ApplySlow(slowDuration, slowMultiplier);

            if (Random.value <= stunChance)
                enemy.ApplyStun(stunDuration);
        }
    }
}