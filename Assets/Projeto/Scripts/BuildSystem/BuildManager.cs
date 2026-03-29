using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public TowerData selectedTower;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        selectedTower = null;
    }

    public void SelectTower(TowerData tower)
    {
        selectedTower = tower;
    }

    public bool CanBuildOn(BuildNode node)
    {
        if (selectedTower == null) return false;
        if (!node.CanBuild()) return false;

        return PlayerResources.instance.CanAfford(selectedTower.costMoney, 0);
    }

    public void BuildOn(BuildNode node)
    {
        if (!CanBuildOn(node)) return;

        PlayerResources.instance.Spend(selectedTower.costMoney, 0);

        node.BuildTower(selectedTower);

        CancelBuild();
    }

    public void CancelBuild()
    {
        selectedTower = null;
    }
}