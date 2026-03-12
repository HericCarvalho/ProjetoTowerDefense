using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public static EnemyPath instance;

    public Transform[] waypoints;

    void Awake()
    {
        instance = this;
    }

    public Transform GetWaypoint(int index)
    {
        if (waypoints == null || index >= waypoints.Length)
            return null;

        return waypoints[index];
    }

    public int WaypointCount()
    {
        return waypoints.Length;
    }
}