using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform target;
    public float speed = 20f;
    public float damage = 50f;

    public GameObject ownerTower;

    private GameObject prefabReference;

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
            enemy.TakeDamage(damage);

            Tower tower = ownerTower.GetComponent<Tower>();

            if (tower != null)
            {
                tower.GainXP(1);
            }
        }

        ReturnToPool();
    }

    void ReturnToPool()
    {
        ObjectPool.instance.ReturnObject(gameObject, prefabReference);
    }
}