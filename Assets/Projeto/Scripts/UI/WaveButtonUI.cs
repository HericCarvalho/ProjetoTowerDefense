using UnityEngine;
using UnityEngine.UI;

public class WaveButtonUI : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        if (startButton != null)
            startButton.onClick.AddListener(StartWave);
        else
            Debug.LogError("StartButton não foi atribuído!");
    }

    void Update()
    {
        if (WaveManager.instance == null || startButton == null)
            return;

        bool waveRunning = WaveManager.instance.IsWaveRunning();
        bool acabou = WaveManager.instance.GetCurrentWaveIndex() >= WaveManager.instance.GetTotalWaves();

        startButton.interactable = !waveRunning && !acabou;
    }

    void StartWave()
    {
        if (WaveManager.instance != null)
            WaveManager.instance.StartWave();
    }
}