using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public TutorialSteps currentStep;

    public bool isTutorialActive = true;

    [Header("UI")]
    public TutorialUI tutorialUI;

    [Header("Referências")]
    public Transform towerIcon;
    public Transform startWaveButton;
    public Transform upgradeButton;

    Transform currentNode;
    Transform placedTower;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        isTutorialActive = (sceneIndex == 1);

        Debug.Log("Tutorial ativo: " + isTutorialActive);

        if (!isTutorialActive)
        {
            tutorialUI.HideAll();
            return;
        }

        Debug.Log("Iniciando tutorial...");
        StartStep(TutorialSteps.OpenShop);
    }

    public void StartStep(TutorialSteps step)
    {
        currentStep = step;

        Debug.Log("STEP: " + step);

        switch (step)
        {
            case TutorialSteps.OpenShop:
                currentNode = FindFirstObjectByType<BuildNode>()?.transform;

                Debug.Log("Node encontrado: " + currentNode);

                tutorialUI.ShowClick(currentNode);
                break;
        }
    }

    public void NextStep()
    {
        if (!isTutorialActive) return;

        int next = (int)currentStep + 1;

        if (next > (int)TutorialSteps.Completed)
        {
            StartStep(TutorialSteps.Completed);
            return;
        }

        StartCoroutine(NextStepRoutine((TutorialSteps)next));
    }

    IEnumerator NextStepRoutine(TutorialSteps step)
    {
        yield return new WaitForSeconds(0.4f);
        StartStep(step);
    }


    public void OnNodeClicked(Transform node)
    {
        if (currentStep != TutorialSteps.OpenShop) return;

        currentNode = node;
        NextStep();
    }

    public void OnTowerDragged()
    {
        if (currentStep != TutorialSteps.DragTower) return;

        NextStep();
    }

    public void OnTowerPlaced(Transform tower)
    {
        if (currentStep != TutorialSteps.PlaceTower) return;

        placedTower = tower;
        NextStep();
    }

    public void OnWaveStarted()
    {
        if (currentStep != TutorialSteps.StartWave) return;

        NextStep();
    }

    public void OnTowerSelected()
    {
        if (currentStep != TutorialSteps.SelectTower) return;

        NextStep();
    }

    public void OnTowerUpgraded()
    {
        if (currentStep != TutorialSteps.UpgradeTower) return;

        NextStep();
    }
}