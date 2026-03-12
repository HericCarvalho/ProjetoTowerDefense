using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 10f;

    public Transform target;

    public string enemyTag = "Enemy";

    public Transform firePoint;

    public GameObject bulletPrefab;

    public float fireRate = 1f;

    private float fireCountdown = 0f;

    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > range)
        {
            target = null;
            return;
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target, gameObject);
        }
    }
}