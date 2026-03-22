using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaUI : MonoBehaviour
{
    public Slider manaSlider;
    public TextMeshProUGUI manaText;

    void Update()
    {
        if (PlayerMana.instance == null)
            return;

        float current = PlayerMana.instance.currentMana;
        float max = PlayerMana.instance.maxMana;

        if (manaSlider != null)
        {
            manaSlider.value = current / max;
        }

        if (manaText != null)
        {
            manaText.text = ((int)current) + " / " + ((int)max);
        }
    }
}