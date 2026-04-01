using UnityEngine;
using TMPro;

public class WinUI : MonoBehaviour
{
    public GameObject[] stars;

    public TextMeshProUGUI killsText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dpsText;
    public TextMeshProUGUI mvpText;

    public void ShowStats(int starsAmount)
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(i < starsAmount);
        }
        killsText.text = "Kills: " + LevelStatsManager.instance.enemiesKilled;

        timeText.text = "Time: " + LevelStatsManager.instance.GetFormattedTime();

        float dps = LevelStatsManager.instance.GetDPS();
        dpsText.text = "DPS: " + dps.ToString("F1");

        Tower mvp = LevelStatsManager.instance.GetMVPTower();

        if (mvp != null)
        {
            float damage = LevelStatsManager.instance.GetTowerDamage(mvp);

            mvpText.text = "MVP: " + mvp.data.name + " (" + damage.ToString("F0") + ")";
        }
        else
        {
            mvpText.text = "MVP: None";
        }
    }
}