using UnityEngine;

public class BuildNode : MonoBehaviour
{
    public GameObject tower;
    public bool isOccupied;
    public Tower currentTower;

    public bool CanBuild()
    {
        if (TutorialBlocker.Instance != null && !TutorialBlocker.Instance.CanClickNode())
            
        BuildMenuUI.instance.OpenMenu(this);

        if (TutorialManager.Instance != null)
        {
            TutorialManager.Instance.OnNodeClicked(transform);
        }

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