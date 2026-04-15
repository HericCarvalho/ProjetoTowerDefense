using UnityEngine;

public class TutorialBlocker : MonoBehaviour
{
    public static TutorialBlocker Instance;

    void Awake()
    {
        Instance = this;
    }

    bool Check(TutorialSteps step)
    {
        if (TutorialManager.Instance == null) return true;

        if (!TutorialManager.Instance.isTutorialActive) return true;

        return TutorialManager.Instance.currentStep == step;
    }

    public bool CanClickNode() => Check(TutorialSteps.OpenShop);
    public bool CanDragTower() => Check(TutorialSteps.DragTower);
    public bool CanPlaceTower() => Check(TutorialSteps.PlaceTower);
    public bool CanStartWave() => Check(TutorialSteps.StartWave);
    public bool CanSelectTower() => Check(TutorialSteps.SelectTower);
    public bool CanUpgradeTower() => Check(TutorialSteps.UpgradeTower);
}