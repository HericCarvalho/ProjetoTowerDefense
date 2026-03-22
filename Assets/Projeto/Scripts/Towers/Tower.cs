using UnityEngine;

public enum TargetMode
{
    First,
    Last,
    Closest,
    Strongest
}

public class Tower : MonoBehaviour
{
    public TowerStats stats;

    public string enemyTag = "Enemy";

    public Transform firePoint;
    public Transform target;

    public TargetMode targetMode = TargetMode.First;

    float fireCountdown = 0f;

    [Header("XP")]
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel;

    void Start()
    {
        stats = GetComponent<TowerStats>();
        xpToNextLevel = stats.data.baseXPToLevel;
    }

    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stats.range)
        {
            target = null;
            return;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / stats.fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void FindTarget()
    {
        Transform bestTarget = null;
        float bestValue = 0f;

        foreach (Transform enemy in EnemyManager.instance.enemies)
        {
            if (!enemy.gameObject.activeInHierarchy)
                continue; 

            float distance = Vector3.Distance(transform.position, enemy.position);
            if (distance > stats.range)
                continue;

            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (movement == null || health == null)
                continue;

            float value = 0f;

            switch (targetMode)
            {
                case TargetMode.First:
                    value = movement.GetProgress();
                    if (bestTarget == null || value > bestValue)
                    {
                        bestValue = value;
                        bestTarget = enemy;
                    }
                    break;

                case TargetMode.Last:
                    value = movement.GetProgress();
                    if (bestTarget == null || value < bestValue)
                    {
                        bestValue = value;
                        bestTarget = enemy;
                    }
                    break;

                case TargetMode.Closest:
                    value = distance;
                    if (bestTarget == null || value < bestValue || bestTarget == null)
                    {
                        bestValue = value;
                        bestTarget = enemy;
                    }
                    break;

                case TargetMode.Strongest:
                    value = health.CurrentHealth;
                    if (bestTarget == null || value > bestValue)
                    {
                        bestValue = value;
                        bestTarget = enemy;
                    }
                    break;
            }
        }

        target = bestTarget;
    }

    void Shoot()
    {
        GameObject bulletGO = ObjectPool.instance.GetObject(stats.bulletPrefab);

        bulletGO.transform.position = firePoint.position;
        bulletGO.transform.rotation = firePoint.rotation;

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        bullet.damage = stats.damage;

        bullet.isMagicDamage = stats.isMagicDamage;
        bullet.isTrueDamage = stats.isTrueDamage;

        bullet.Seek(target, gameObject, stats.bulletPrefab);
    }

    public void GainXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentXP -= xpToNextLevel;
        level++;

        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);

        Debug.Log("Tower Level Up: " + level);

        if (level % 5 == 0)
        {
            Debug.Log("Evolution Available!");
        }
    }
}