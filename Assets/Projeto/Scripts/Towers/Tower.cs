using UnityEngine;

public enum AttackType
{
    Projectile,
    Laser,
    Earthquake
}

public class Tower : MonoBehaviour
{
    public TowerData data;

    public Transform head;
    public float rotationSpeed = 10f; 
    public Transform firePoint;
    public Transform target;

    public GameObject bulletPrefab;

    public AttackType attackType;

    float fireCountdown = 0f;

    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel;

    public int upgradePoints = 0;

    float bonusDamage;
    float bonusRange;
    float bonusFireRate;

    void Start()
    {
        xpToNextLevel = data.baseXPToLevel;
    }

    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > GetRange())
        {
            target = null;
            return;
        }

        if (attackType == AttackType.Laser)
        {
            LaserAttack();
            return;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / GetFireRate();
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        if (attackType == AttackType.Projectile)
            ProjectileAttack();

        if (attackType == AttackType.Earthquake)
            EarthquakeAttack();
    }

    void ProjectileAttack()
    {
        GameObject bulletGO = ObjectPool.instance.GetObject(bulletPrefab);

        bulletGO.transform.position = firePoint.position;
        bulletGO.transform.rotation = firePoint.rotation;

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        bullet.Seek(target, gameObject, bulletPrefab);

        bullet.damage += GetBonusDamage();
    }

    void LaserAttack()
    {
        if (target == null) return;

        EnemyHealth enemy = target.GetComponent<EnemyHealth>();
        if (enemy == null) return;

        float damage = GetBonusDamage() * Time.deltaTime;

        enemy.TakeDamage(damage, false, false);
    }

    void EarthquakeAttack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 3f);

        foreach (Collider col in hits)
        {
            EnemyHealth enemy = col.GetComponent<EnemyHealth>();
            if (enemy == null) continue;

            enemy.TakeDamage(GetBonusDamage(), false, false);
        }
    }

    void FindTarget()
    {
        Transform bestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform enemy in EnemyManager.instance.enemies)
        {
            if (!enemy.gameObject.activeInHierarchy)
                continue;

            float distance = Vector3.Distance(transform.position, enemy.position);

            if (distance < shortestDistance && distance <= GetRange())
            {
                shortestDistance = distance;
                bestTarget = enemy;
            }
        }

        target = bestTarget;
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

        upgradePoints++;
    }

    public float GetRange()
    {
        return data.baseRange + (data.rangePerLevel * (level - 1)) + bonusRange;
    }

    public float GetFireRate()
    {
        return data.baseFireRate + (data.fireRatePerLevel * (level - 1)) + bonusFireRate;
    }

    public float GetBonusDamage()
    {
        return bonusDamage;
    }

    public void AddDamage(float amount)
    {
        bonusDamage += amount;
    }

    public void AddRange(float amount)
    {
        bonusRange += amount;
    }

    public void AddFireRate(float amount)
    {
        bonusFireRate += amount;
    }

    public void TryEvolve()
    {
        if (level < 5) return;
        if (data.nextUpgrade == null) return;

        int cost = 20;

        if (!PlayerResources.instance.CanAfford(0, cost))
            return;

        PlayerResources.instance.Spend(0, cost);

        Instantiate(data.nextUpgrade.prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public void UpgradeDamage(float amount)
    {
        if (upgradePoints <= 0) return;

        bonusDamage += amount;
        upgradePoints--;
    }

    public void UpgradeRange(float amount)
    {
        if (upgradePoints <= 0) return;

        bonusRange += amount;
        upgradePoints--;
    }

    public void UpgradeFireRate(float amount)
    {
        if (upgradePoints <= 0) return;

        bonusFireRate += amount;
        upgradePoints--;
    }
    public void Transmute(TowerData option)
    {
        if (level < 10) return;
        if (option == null) return;

        Instantiate(option.prefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public void Sell()
    {

    }
    public void OnSelected()
    {
        TowerUIManager.instance.SelectTower(this);
    }
    void RotateTowardsTarget()
    {
        if (target == null || head == null)
            return;

        Vector3 direction = target.position - head.position;

        direction.y = 0f;

        if (direction == Vector3.zero)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        head.rotation = Quaternion.Lerp(
            head.rotation,
            lookRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}