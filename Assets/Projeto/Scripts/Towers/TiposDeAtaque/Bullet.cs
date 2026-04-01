using TMPro;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 20f;
    public float damage = 50f;

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

    float baseDamage;

    private Transform target;
    private EnemyHealth cachedEnemy;

    private GameObject prefabReference;
    private Tower ownerTower;

    Vector3 direction;
    Vector3 targetPosition;
    bool usePredicted = false;

    public void Seek(Transform _target, GameObject tower, GameObject prefab)
    {
        target = _target;
        prefabReference = prefab;

        if (target == null) return;

        EnemyMovement em = target.GetComponent<EnemyMovement>();

        Vector3 predictedPos = target.position;

        if (em != null)
        {
            Vector3 enemyVelocity = em.GetVelocity();

            float distance = Vector3.Distance(transform.position, target.position);
            float timeToHit = distance / speed;

            predictedPos = target.position + enemyVelocity * timeToHit;
        }

        direction = (predictedPos - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0);


        cachedEnemy = target.GetComponent<EnemyHealth>();

        if (tower != null)
            ownerTower = tower.GetComponent<Tower>();
    }
    public void SeekPosition(Vector3 pos, Transform _target, GameObject tower, GameObject prefab)
    {
        targetPosition = pos;
        target = _target;
        prefabReference = prefab;

        cachedEnemy = _target != null ? _target.GetComponent<EnemyHealth>() : null;
        ownerTower = tower != null ? tower.GetComponent<Tower>() : null;

        usePredicted = true;
    }

    void Awake()
    {
        baseDamage = damage;
    }

    void OnEnable()
    {
        damage = baseDamage;
        cachedEnemy = null;
        ownerTower = null;
    }
    void Update()
    {
        if (target == null)
        {
            ReturnToPool();
            return;
        }

        Vector3 aimPoint = usePredicted ? targetPosition : target.position;
        Vector3 offset = target.forward * -4f;
        Vector3 dir = (target.position + offset) - transform.position;
        Vector3 move = dir.normalized * speed * Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 90, 0);

        transform.position += move;

        if (move.magnitude >= dir.magnitude)
        {
            HitTarget();
        }
    }
    void HitTarget()
    {
        if (cachedEnemy != null)
        {
            cachedEnemy.TakeDamage(damage, isMagicDamage, isTrueDamage);
            LevelStatsManager.instance.RegisterDamage(ownerTower, damage);

            if (Random.value <= burnChance)
                cachedEnemy.ApplyBurn(burnDuration, burnDPS);

            if (Random.value <= slowChance)
                cachedEnemy.ApplySlow(slowDuration, slowMultiplier);

            if (Random.value <= stunChance)
                cachedEnemy.ApplyStun(stunDuration);

            if (ownerTower != null)
                ownerTower.GainXP(1);
        }

        ReturnToPool();
    }

    void ReturnToPool()
    {
        if (prefabReference == null)
        {
            Debug.LogWarning("Bullet sem prefabReference!");
            gameObject.SetActive(false);
            return;
        }

        ObjectPool.instance.ReturnObject(gameObject, prefabReference);
    }
}