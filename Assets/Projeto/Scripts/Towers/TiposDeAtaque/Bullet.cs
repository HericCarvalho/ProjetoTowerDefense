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

    public GameObject ownerTower;
    private GameObject prefabReference;

    Transform target;
    public void Seek(Transform _target, GameObject tower, GameObject prefab)
    {
        target = _target;
        ownerTower = tower;
        prefabReference = prefab;
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
        EnemyHealth enemy = target.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage, isMagicDamage, isTrueDamage);

            if (Random.value <= burnChance)
                enemy.ApplyBurn(burnDuration, burnDPS);

            if (Random.value <= slowChance)
                enemy.ApplySlow(slowDuration, slowMultiplier);

            if (Random.value <= stunChance)
                enemy.ApplyStun(stunDuration);

            Tower tower = ownerTower.GetComponent<Tower>();
            if (tower != null)
                tower.GainXP(1);
        }

        ReturnToPool();
    }

    void ReturnToPool()
    {
        ObjectPool.instance.ReturnObject(gameObject, prefabReference);
    }
}