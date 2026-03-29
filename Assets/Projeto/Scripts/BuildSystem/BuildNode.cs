using UnityEngine;

public class BuildNode : MonoBehaviour
{
    public GameObject tower;

    public bool CanBuild()
    {
        return tower == null;
    }

    public void BuildTower(TowerData data)
    {
        tower = Instantiate(data.prefab, transform.position, Quaternion.identity);
    }

    public bool HasTower()
    {
        return tower != null;
    }
}