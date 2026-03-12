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
            return;

        Transform target = EnemyPath.instance.GetWaypoint(waypointIndex);
        Vector3 dir = target.position - transform.position;

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            waypointIndex++;
        }
    }

    public float GetProgress()
    {
        return waypointIndex;
    }
}