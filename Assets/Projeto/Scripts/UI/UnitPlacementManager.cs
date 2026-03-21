using UnityEngine;

public class UnitPlacementManager : MonoBehaviour
{
    public static UnitPlacementManager instance;

    void Awake()
    {
        instance = this;
    }

    bool IsOnPath(Vector3 position)
    {
        Collider[] hits = Physics.OverlapSphere(position, 0.5f);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Path"))
                return true;
        }

        return false;
    }

    public void SpawnUnit(GameObject unitPrefab, Vector3 position)
    {
        if (unitPrefab == null)
            return;

        if (!FragmentManager.instance.HasFragment(unitPrefab))
            return;

        if (!IsOnPath(position))
        {
            Debug.Log(" Só pode spawnar no caminho!");
            return;
        }

        GameObject unit = Instantiate(unitPrefab, position, Quaternion.identity);

        FragmentManager.instance.UseFragment(unitPrefab);
    }
}