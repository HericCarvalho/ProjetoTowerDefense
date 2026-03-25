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

    private Transform target;
    private EnemyHealth cachedEnemy;

    private GameObject prefabReference;
    private Tower ownerTower;

    public void Seek(Transform _target, GameObject tower, GameObject prefab)
    {
        target = _target;
        prefabReference = prefab;

        if (_target != null)
            cachedEnemy = _target.GetComponent<EnemyHealth>();

        if (tower != null)
            ownerTower = tower.GetComponent<Tower>();
    }

    void OnEnable()
    {
        cachedEnemy = null;
        target = null;
        ownerTower = null;
    }
    void Update()
    {
        if (target == null)
        {
            ReturnToPool();
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }
    void HitTarget()
    {
        if (cachedEnemy != null)
        {
            cachedEnemy.TakeDamage(damage, isMagicDamage, isTrueDamage);

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