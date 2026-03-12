using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 20f;
    public float damage = 50f;

    public GameObject ownerTower;

    public void Seek(Transform _target, GameObject tower)
    {
        target = _target;
        ownerTower = tower;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
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

            if (enemy.health <= 0)
            {
                EnemyXPReward xp = target.GetComponent<EnemyXPReward>();

                if (xp != null)
                {
                    xp.GiveXP(ownerTower);
                }
            }
        }

        Destroy(gameObject);
    }
}