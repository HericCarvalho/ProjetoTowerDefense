using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveInfoUI : MonoBehaviour
{
    public TextMeshProUGUI waveText;

    void Update()
    {
        int current = WaveManager.instance.GetCurrentWaveIndex() + 1;
        int total = WaveManager.instance.GetTotalWaves();

        waveText.text = "Wave " + current + "/" + total;
    }

}