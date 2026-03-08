using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        EnemyReward reward = GetComponent<EnemyReward>();

        if (reward != null)
        {
            reward.GiveReward();
        }

        Destroy(gameObject);
    }
}