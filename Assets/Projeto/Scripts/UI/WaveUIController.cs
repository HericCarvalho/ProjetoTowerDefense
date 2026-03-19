using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveUIController : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText;
    void Update()
    {
        if (WaveManager.instance == null)
            return;

        if (WaveManager.instance.IsWaveRunning())
        {
            int remaining = WaveManager.instance.GetRemainingEnemies();
            int total = WaveManager.instance.GetTotalEnemiesInWave();

            enemyCountText.text = "Enemies: " + remaining + " / " + total;
        }
        else
        {
            EnemyWave wave = WaveManager.instance.GetCurrentWave();

            if (wave == null)
            {
                enemyCountText.text = "All waves completed!";
                return;
            }

            string preview = "";

            foreach (EnemyGroup group in wave.groups)
            {
                preview += group.enemyPrefab.name + " x" + group.count + "\n";
            }

            enemyCountText.text = preview;
        }
    }
}