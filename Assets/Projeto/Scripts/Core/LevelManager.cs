using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public WinUI winUI;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject reviveHUD;

    bool isReviveOpen = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LevelStatsManager.instance.StartLevel();
    }

    public void WinGame()
    {
        if (LevelStatsManager.instance != null)
        {
            LevelStatsManager.instance.EndLevel();
        }

        int stars = LevelStatsManager.instance.GetStars();

        GameManager.instance.UnlockNextLevel();

        winUI.ShowStats(stars);

        Time.timeScale = 0f;
        winPanel.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void ToggleReviveHUD()
    {
        isReviveOpen = !isReviveOpen;
        reviveHUD.SetActive(isReviveOpen);
    }

    public void OpenReviveHUD()
    {
        isReviveOpen = true;
        reviveHUD.SetActive(true);
    }

    public void CloseReviveHUD()
    {
        isReviveOpen = false;
        reviveHUD.SetActive(false);
    }

    public bool IsReviveOpen()
    {
        return isReviveOpen;
    }
}