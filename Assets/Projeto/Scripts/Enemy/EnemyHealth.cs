using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    public float damage = 5f;
    public float attackRate = 1f;
    public float attackRange = 1.5f;
    public float moveSpeed = 5f;
    public float maxHealth = 100f;

    [Header("Resistances (0 a 1)")]
    [Range(0f, 1f)] public float physicalResistance;
    [Range(0f, 1f)] public float magicalResistance;

    [Header("Attack Type")]
    public bool isRanged;
    public bool isMagical;

    [Header("Effects")]
    private bool isBurning;
    private float burnTimer;
    private float burnDamagePerSecond;
    private bool isSlowed;
    private float slowTimer;
    private float slowMultiplier = 1f;
    public bool isStunned;
    private float stunTimer;

    [Header("Effect Resistances")]
    [Range(0f, 1f)] public float burnResistance;
    [Range(0f, 1f)] public float slowResistance;
    [Range(0f, 1f)] public float stunResistance;

    [Header("Effect Immunities")]
    public bool immuneToBurn;
    public bool immuneToSlow;
    public bool immuneToStun;

    [Header("Visual Effects")]
    public GameObject burnEffect;
    public GameObject slowEffect;
    public GameObject stunEffect;

    [Header("Fragment Drop")]
    public string enemyID;
    [Range(0f, 1f)] public float fragmentDropChance = 0.3f;

    [Header("Damage Popup")]
    public GameObject damagePopupPrefab;

    private float burnPopupTimer;
    private float health;
    private float attackCooldown;
    private Transform combatTarget;

    private GameObject prefabReference;



    void OnEnable()
    {
        health = maxHealth;

        isBurning = false;
        isSlowed = false;
        isStunned = false;

        if (burnEffect != null) burnEffect.SetActive(false);
        if (slowEffect != null) slowEffect.SetActive(false);
        if (stunEffect != null) stunEffect.SetActive(false);

        EnemyManager.instance.RegisterEnemy(transform);
    }
    void Update()
    {
        HandleBurn();
        HandleSlow();
        HandleStun();
        HandleCombat();
    }

    public void SetPrefabReference(GameObject prefab)
    {
        prefabReference = prefab;
    }
    public float CurrentHealth => health;
    public void TakeDamage(float amount, bool isMagicDamage, bool isTrueDamage = false)
    {
        float finalDamage = amount;

        if (!isTrueDamage)
        {
            if (isMagicDamage)
            {
                finalDamage *= (1f - magicalResistance);
            }
            else
            {
                finalDamage *= (1f - physicalResistance);
            }
            ShowDamagePopup((int)finalDamage, isMagicDamage, isTrueDamage);
        }
        else
        {

        }


        health -= finalDamage;

        if (health <= 0)
            Die();
    }
    public void SetTarget(Transform target)
    {
        combatTarget = target;
    }
    void HandleCombat()
    {
        if (combatTarget == null)
            return;

        float distance = Vector3.Distance(transform.position, combatTarget.position);

        if (distance > attackRange)
            return;

        attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0f)
        {
            RevivedUnit unit = combatTarget.GetComponent<RevivedUnit>();

            if (unit != null)
            {
                unit.TakeDamage(damage);
            }

            attackCooldown = 1f / attackRate;
        }
    }
    void Die()
    {
        EnemyManager.instance.UnregisterEnemy(transform);
        ObjectPool.instance.ReturnObject(gameObject, prefabReference);
        WaveManager.instance.RegisterEnemyDeath();

        EnemyReward reward = GetComponent<EnemyReward>();
        if (reward != null)
            reward.GiveReward();

        TryDropFragment();
    }
    void ShowDamagePopup(int damage, bool isMagic, bool isTrue, bool isBurn = false)
    {
        if (damagePopupPrefab == null) return;

        GameObject popupGO = ObjectPool.instance.GetObject(damagePopupPrefab);

        popupGO.transform.position = transform.position + Vector3.up * 2f;

        DamagePopup popup = popupGO.GetComponent<DamagePopup>();

        Color color = Color.white;

        if (isBurn)
            color = Color.red;
        else if (isTrue)
            color = Color.white;
        else if (isMagic)
            color = Color.blue;
        else
            color = Color.gray;

        popup.Setup(damage, color, damagePopupPrefab);
    }
    public GameObject GetPrefab()
    {
        return prefabReference;
    }
    void HandleBurn()
    {
        if (!isBurning) return;

        burnTimer -= Time.deltaTime;

        float damageThisFrame = burnDamagePerSecond * Time.deltaTime;
        health -= damageThisFrame;

        burnPopupTimer -= Time.deltaTime;
        if (burnPopupTimer <= 0f)
        {
            ShowDamagePopup((int)burnDamagePerSecond, false, true, true);
            burnPopupTimer = 0.5f;
        }

        if (burnTimer <= 0f)
        {
            isBurning = false;

            if (burnEffect != null)
                burnEffect.SetActive(false);
        }
    }
    public void ApplyBurn(float duration, float dps)
    {
        if (immuneToBurn) return;

        float finalChance = 1f - burnResistance;

        if (Random.value > finalChance)
            return; 

        isBurning = true;
        burnTimer = duration;
        burnDamagePerSecond = dps;

        if (burnEffect != null)
            burnEffect.SetActive(true);
    }

    void HandleSlow()
    {
        if (!isSlowed) return;

        slowTimer -= Time.deltaTime;

        if (slowTimer <= 0f)
        {
            isSlowed = false;
            slowMultiplier = 1f;

            if (slowEffect != null)
                slowEffect.SetActive(false);
        }
    }
    public void ApplySlow(float duration, float multiplier)
    {
        if (immuneToSlow) return;

        float finalChance = 1f - slowResistance;

        if (Random.value > finalChance)
            return;

        isSlowed = true;
        slowTimer = duration;
        slowMultiplier = multiplier;

        if (slowEffect != null)
            slowEffect.SetActive(true);
    }
    public float GetSlowMultiplier()
    {
        return slowMultiplier;
    }

    void HandleStun()
    {
        if (!isStunned) return;

        stunTimer -= Time.deltaTime;

        if (stunTimer <= 0f)
        {
            isStunned = false;

            if (stunEffect != null)
                stunEffect.SetActive(false);
        }
    }
    public void ApplyStun(float duration)
    {
        if (immuneToStun) return;

        float finalChance = 1f - stunResistance;

        if (Random.value > finalChance)
            return;

        isStunned = true;
        stunTimer = duration;

        if (stunEffect != null)
            stunEffect.SetActive(true);
    }
    public bool IsStunned()
    {
        return isStunned;
    }

    void TryDropFragment()
    {
        if (string.IsNullOrEmpty(enemyID))
        {
            Debug.LogWarning("Enemy sem enemyID!");
            return;
        }

        float roll = Random.value;

        if (roll <= fragmentDropChance)
        {
            FragmentManager.instance.AddFragment(enemyID);

            Debug.Log($"DROP! {enemyID} ({roll})");
        }
        else
        {
            Debug.Log($"No Drop {enemyID} ({roll})");
        }
    }

}