using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStatsManager : MonoBehaviour
{
    public Dictionary<Tower, float> towerDamage = new Dictionary<Tower, float>();

    public static LevelStatsManager instance;

    public int enemiesKilled;
    public float levelTime;
    public float totalDamageDealt;


    bool countingTime = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (countingTime)
        {
            levelTime += Time.deltaTime;
        }
    }

    public void StartLevel()
    {
        enemiesKilled = 0;
        levelTime = 0f;
        countingTime = true;
    }

    public void RegisterKill()
    {
        enemiesKilled++;
    }
    public void RegisterDamage(Tower tower, float damage)
    {
        totalDamageDealt += damage;

        if (tower == null) return;

        if (!towerDamage.ContainsKey(tower))
            towerDamage[tower] = 0;

        towerDamage[tower] += damage;
    }

    public void EndLevel()
    {
        countingTime = false;

        int stars = CalculateStars();

        SaveBestResult(stars);
        PlayerStars.instance.AddStars(stars);

        Debug.Log($"Stars: {stars} | Time: {levelTime} | Kills: {enemiesKilled}");
    }

    int CalculateStars()
    {
        float percent = (float)BaseHealth.instance.currentHealth / BaseHealth.instance.maxHealth;

        if (percent >= 1f)
            return 3;
        else if (percent > 0.5f)
            return 2;
        else
            return 1;
    }

    void SaveBestResult(int stars)
    {
        string levelKey = "LEVEL_" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        int bestStars = PlayerPrefs.GetInt(levelKey + "_STARS", 0);

        if (stars > bestStars)
        {
            int difference = stars - bestStars;

            if (PlayerStars.instance != null)
            {
                PlayerStars.instance.AddStars(difference);
            }

            PlayerPrefs.SetInt(levelKey + "_STARS", stars);
        }

        PlayerPrefs.SetFloat(levelKey + "_TIME", levelTime);
        PlayerPrefs.SetInt(levelKey + "_KILLS", enemiesKilled);

        PlayerPrefs.Save();
    }
    public int GetStars()
    {
        float percent = (float)BaseHealth.instance.currentHealth / BaseHealth.instance.maxHealth;

        if (percent >= 1f)
            return 3;
        else if (percent > 0.5f)
            return 2;
        else
            return 1;
    }
    public float GetDPS()
    {
        if (levelTime <= 0f) return 0f;

        return totalDamageDealt / levelTime;
    }
    public float GetTowerDamage(Tower tower)
    {
        if (tower == null) return 0f;

        if (towerDamage.ContainsKey(tower))
            return towerDamage[tower];

        return 0f;
    }
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(levelTime / 60);
        int seconds = Mathf.FloorToInt(levelTime % 60);

        return $"{minutes:00}:{seconds:00}";
    }
    public Tower GetMVPTower()
    {
        Tower bestTower = null;
        float maxDamage = 0f;

        foreach (var pair in towerDamage)
        {
            if (pair.Value > maxDamage)
            {
                maxDamage = pair.Value;
                bestTower = pair.Key;
            }
        }

        return bestTower;
    }
}