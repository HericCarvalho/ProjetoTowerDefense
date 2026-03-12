using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject towerPrefab;

    void Awake()
    {
        instance = this;
    }

    public GameObject GetTowerToBuild()
    {
        return towerPrefab;
    }
}