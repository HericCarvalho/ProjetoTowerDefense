using UnityEngine;

public class BuildNode : MonoBehaviour
{
    public GameObject tower;
    public bool isOccupied;
    public Tower currentTower;

    public bool CanBuild()
    {
        BuildMenuUI.instance.OpenMenu(this);

        return tower == null;

    }

    public GameObject BuildTower(TowerData towerData)
    {
        GameObject tower = Instantiate(towerData.prefab, transform.position, Quaternion.identity);
        return tower;
    }

    public bool HasTower()
    {
        return tower != null;
    }
}