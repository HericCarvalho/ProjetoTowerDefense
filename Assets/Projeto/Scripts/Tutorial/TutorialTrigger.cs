using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public void Trigger(TutorialSteps step)
    {
        if (TutorialManager.Instance == null) return;

        TutorialManager.Instance.NextStep();
    }
}