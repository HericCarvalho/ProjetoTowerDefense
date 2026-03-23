using UnityEngine;

public class BuildNode : MonoBehaviour
{
    public GameObject tower;

    public void BuildTower(TowerData data)
    {
        if (tower != null)
            return;

        if (!PlayerResources.instance.CanAfford(data.costMoney, data.costRestos))
        {
            return;
        }

        PlayerResources.instance.Spend(data.costMoney, data.costRestos);

        tower = Instantiate(data.prefab, transform.position, Quaternion.identity);
    }
}