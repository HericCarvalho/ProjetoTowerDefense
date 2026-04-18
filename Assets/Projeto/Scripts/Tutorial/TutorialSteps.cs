using UnityEngine;

[System.Serializable]
public class TutorialSteps
{
    [TextArea] public string text;

    public Transform target;

    public bool useCircle = true;
    public bool useArrow = false;

    public Vector3 offset;

    public float delayBefore = 0.2f;
}