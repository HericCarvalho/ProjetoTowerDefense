using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarsUI : MonoBehaviour
{
    public TextMeshProUGUI starsText;

    void Update()
    {
        starsText.text = $"* {PlayerStars.instance.totalStars}";
    }
}