using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public TutorialSteps currentStep;

    [Header("Controle")]
    public bool isTutorialActive = true;

    [Header("Referências")]
    public TutorialUI tutorialUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (!isTutorialActive)
            return;

        StartStep(TutorialSteps.OpenShop);
    }

    public void StartStep(TutorialSteps step)
    {
        if (!isTutorialActive)
            return;

        currentStep = step;

        Debug.Log("Tutorial Step: " + step);

        switch (step)
        {
            case TutorialSteps.OpenShop:
                tutorialUI.ShowClick(tutorialUI.shopButton);
                break;

            case TutorialSteps.DragTower:
                tutorialUI.ShowDrag(tutorialUI.towerIcon, tutorialUI.buildSpot);
                break;

            case TutorialSteps.PlaceTower:
                tutorialUI.HideHand();
                break;

            case TutorialSteps.StartWave:
                tutorialUI.ShowClick(tutorialUI.startWaveButton);
                break;

            case TutorialSteps.SelectTower:
                tutorialUI.ShowClick(tutorialUI.placedTower);
                break;

            case TutorialSteps.UpgradeTower:
                tutorialUI.ShowClick(tutorialUI.upgradeButton);
                break;

            case TutorialSteps.Completed:
                tutorialUI.HideAll();
                Debug.Log("Tutorial finalizado!");
                break;
        }
    }

    public void NextStep()
    {
        if (!isTutorialActive)
            return;

        int next = (int)currentStep + 1;
        StartStep((TutorialSteps)next);
    }
}