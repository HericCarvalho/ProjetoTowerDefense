using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedButton : MonoBehaviour
{
    public TextMeshProUGUI label;

    private float[] speeds = { 1f, 2f, 3f };
    private int index = 0;

    public void ToggleSpeed()
    {
        index = (index + 1) % speeds.Length;

        Time.timeScale = speeds[index];
        Time.fixedDeltaTime = 0.02f * speeds[index];

        label.text = speeds[index] + "x";
    }
}