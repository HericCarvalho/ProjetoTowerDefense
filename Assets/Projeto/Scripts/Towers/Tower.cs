using System.Collections.Generic;
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
    public EarthquakeAttack earthquakeAttack;

    public Transform head;
    public float rotationSpeed = 10f; 
    public Transform firePoint;
    public Transform target;

    public GameObject bulletPrefab;
    public GameObject rangeIndicator;
    public float rangeVisualMultiplier = 1f;


    public AttackType attackType;

    float fireCountdown = 0f;


    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel;

    public int upgradePoints = 0;

    float bonusDamage;
    float bonusRange;
    float bonusFireRate;

    private float baseSize;
    private GameObject rangeIndicatorInstance;

    void Start()
    {
        xpToNextLevel = data.baseXPToLevel;

        if (rangeIndicator != null)
        {
            rangeIndicatorInstance = Instantiate(rangeIndicator);

            Renderer r = rangeIndicatorInstance.GetComponent<Renderer>();
            if (r != null)
                baseSize = r.bounds.size.x;

            rangeIndicatorInstance.SetActive(false);
        }
    }

    void Update()
    {
        if (target == null)
        {
            FindTarget();
        }

        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > GetRange())
            {
                target = null;
            }
            else
            {
                RotateTowardsTarget();
            }
        }

        if (attackType == AttackType.Laser)
        {
            if (target != null)
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

        if (attackType == AttackType.Projectile && target != null)
        {
            ProjectileAttack();
        }

        if (attackType == AttackType.Earthquake && target != null)
        {
            EarthquakeAttack();
        }
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
        if (earthquakeAttack != null)
        {
            earthquakeAttack.Execute(
                transform.position,
                GetBonusDamage(),
                GetRange(),
                this
            );
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
    public void ShowRange()
    {
        if (rangeIndicatorInstance == null) return;

        rangeIndicatorInstance.SetActive(true);

        Vector3 pos = transform.position;
        pos.y = 0.05f;

        rangeIndicatorInstance.transform.position = pos;

        float diameter = GetRange() * 2f;
        float scaleFactor = diameter / baseSize;

        rangeIndicatorInstance.transform.localScale = new Vector3(
            scaleFactor,
            1,
            scaleFactor
        );
    }

    public void HideRange()
    {
        if (rangeIndicatorInstance == null) return;

        rangeIndicatorInstance.SetActive(false);
    }
}