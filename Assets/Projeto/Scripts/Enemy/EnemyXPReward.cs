using UnityEngine;

public class EnemyXPReward : MonoBehaviour
{
    public int xpReward = 5;

    public void GiveXP(GameObject tower)
    {
        TowerXP towerXP = tower.GetComponent<TowerXP>();

        if (towerXP != null)
        {
            towerXP.GainXP(xpReward);
        }
    }
}