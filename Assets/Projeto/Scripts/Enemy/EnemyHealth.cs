using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health;

    private GameObject prefabReference;


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

    }
    public GameObject GetPrefab()
    {
        return prefabReference;
    }
}