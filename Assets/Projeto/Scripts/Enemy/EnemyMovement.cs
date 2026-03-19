using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;
    private int waypointIndex = 0;

    void OnEnable()
    {
        waypointIndex = 0; 
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (waypointIndex >= EnemyPath.instance.WaypointCount())
        {
            ReachEnd();
            return;
        }

        Transform target = EnemyPath.instance.GetWaypoint(waypointIndex);

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            waypointIndex++;
        }
    }

    void ReachEnd()
    {
        if (BaseHealth.instance != null)
        {
            BaseHealth.instance.TakeDamage(1);
        }
        else
        {
            Debug.LogError("BaseHealth.instance está NULL!");
        }

        EnemyHealth eh = GetComponent<EnemyHealth>();

        if (eh == null)
        {
            Debug.LogError("EnemyHealth não encontrado!");
            return;
        }

        GameObject prefab = eh.GetPrefab();

        if (prefab == null)
        {
            Debug.LogError("PrefabReference está NULL no EnemyHealth!");
            return;
        }

        if (ObjectPool.instance != null)
        {
            ObjectPool.instance.ReturnObject(gameObject, prefab);
        }
        else
        {
            Debug.LogError("ObjectPool.instance está NULL!");
        }
    }

    public float GetProgress()
    {
        return waypointIndex;
    }
}