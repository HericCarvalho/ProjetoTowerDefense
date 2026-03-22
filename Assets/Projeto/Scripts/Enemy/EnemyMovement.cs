using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    private int waypointIndex = 0;

    bool hasReachedEnd = false;
    private bool isInCombat = false;


    private EnemyHealth stats;

    void Awake()
    {
        stats = GetComponent<EnemyHealth>();
    }

    void OnEnable()
    {
        waypointIndex = 0;
        hasReachedEnd = false;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {

        if (hasReachedEnd || isInCombat)
            return;

        if (stats.IsStunned())
            return;

        if (waypointIndex >= EnemyPath.instance.WaypointCount())
        {
            ReachEnd();
            return;
        }

        Transform target = EnemyPath.instance.GetWaypoint(waypointIndex);

        Vector3 dir = target.position - transform.position;

        float speed = stats.moveSpeed * stats.GetSlowMultiplier();

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            waypointIndex++;
        }
    }

    void ReachEnd()
    {
        WaveManager.instance.RegisterEnemyDeath();

        hasReachedEnd = true;

        if (BaseHealth.instance != null)
            BaseHealth.instance.TakeDamage(1);

        EnemyHealth eh = GetComponent<EnemyHealth>();

        if (eh != null)
        {
            GameObject prefab = eh.GetPrefab();

            if (prefab != null && ObjectPool.instance != null)
            {
                ObjectPool.instance.ReturnObject(gameObject, prefab);
            }
        }
    }

    public float GetProgress()
    {
        return waypointIndex;
    }

    public void SetCombat(bool value)
    {
        isInCombat = value;
    }
}