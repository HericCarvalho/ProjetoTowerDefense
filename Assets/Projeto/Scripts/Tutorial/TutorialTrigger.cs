using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    TutorialManager manager;

    private void Awake()
    {
        manager = FindFirstObjectByType<TutorialManager>();
    }

    bool CanRun()
    {
        return manager != null && manager.isTutorialActive;
    }

    public void TowerDragged()
    {
        if (!CanRun()) return;

        if (manager.currentStep == TutorialSteps.DragTower)
            manager.NextStep();
    }

    public void TowerPlaced()
    {
        if (!CanRun()) return;

        if (manager.currentStep == TutorialSteps.PlaceTower)
            manager.NextStep();
    }

    public void ShopOpened()
    {
        if (!CanRun()) return;

        if (manager.currentStep == TutorialSteps.OpenShop)
            manager.NextStep();
    }

    public void WaveStarted()
    {
        if (!CanRun()) return;

        if (manager.currentStep == TutorialSteps.StartWave)
            manager.NextStep();
    }

    public void TowerSelected()
    {
        if (!CanRun()) return;

        if (manager.currentStep == TutorialSteps.SelectTower)
            manager.NextStep();
    }

    public void TowerUpgraded()
    {
        if (!CanRun()) return;

        if (manager.currentStep == TutorialSteps.UpgradeTower)
            manager.NextStep();
    }
}