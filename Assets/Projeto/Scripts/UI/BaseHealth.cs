using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public static BaseHealth instance;

    public int maxHealth = 20;
    public int currentHealth;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        UIManager.instance.UpdateHealth(currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        UIManager.instance.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        LevelManager.instance.GameOver();
    }
}