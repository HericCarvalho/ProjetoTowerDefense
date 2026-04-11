using UnityEngine;

public class TutorialBlocker : MonoBehaviour
{
    public bool CanOpenShop()
    {
        return TutorialManager.Instance.currentStep == TutorialSteps.OpenShop;
    }

    public bool CanDragTower()
    {
        return TutorialManager.Instance.currentStep == TutorialSteps.DragTower;
    }

    public bool CanPlaceTower()
    {
        return TutorialManager.Instance.currentStep == TutorialSteps.PlaceTower;
    }

    public bool CanStartWave()
    {
        return TutorialManager.Instance.currentStep == TutorialSteps.StartWave;
    }

    public bool CanSelectTower()
    {
        return TutorialManager.Instance.currentStep == TutorialSteps.SelectTower;
    }

    public bool CanUpgradeTower()
    {
        return TutorialManager.Instance.currentStep == TutorialSteps.UpgradeTower;
    }
}
