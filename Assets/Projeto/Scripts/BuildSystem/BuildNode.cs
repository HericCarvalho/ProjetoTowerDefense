using UnityEngine;

public class BuildNode : MonoBehaviour
{
    public GameObject tower;

    public void BuildTower(TowerData data)
    {
        if (tower != null)
            return;

        if (!PlayerResources.instance.CanAfford(data.costMoney, 0))
            return;

        PlayerResources.instance.Spend(data.costMoney, 0);

        tower = Instantiate(data.prefab, transform.position, Quaternion.identity);
    }

    public bool HasTower()
    {
        return tower != null;
    }
}