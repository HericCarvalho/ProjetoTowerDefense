using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health;

    private GameObject prefabReference;

    public GameObject fragmentPrefab;
    [Range(0f, 1f)] public float dropChance = 0.2f;


    void OnEnable()
    {
        health = maxHealth;
        EnemyManager.instance.RegisterEnemy(transform);
    }

    public void SetPrefabReference(GameObject prefab)
    {
        prefabReference = prefab;
    }

    public float CurrentHealth => health;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    void Die()
    {
        EnemyManager.instance.UnregisterEnemy(transform);
        ObjectPool.instance.ReturnObject(gameObject, prefabReference);
        WaveManager.instance.RegisterEnemyDeath();

        EnemyReward reward = GetComponent<EnemyReward>();
        if (reward != null)
            reward.GiveReward();
        if (Random.value <= dropChance)
        {
            FragmentManager.instance.AddFragment(fragmentPrefab);
        }

    }
    public GameObject GetPrefab()
    {
        return prefabReference;
    }
}