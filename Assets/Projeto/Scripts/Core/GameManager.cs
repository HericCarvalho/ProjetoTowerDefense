using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject winPanel;
    public GameObject gameOverPanel;

    public GameObject reviveHUD;

    [Header("Sistema de Estrelas")]
    public GameObject[] starImages;
    public int currentLevelID;

    private bool isReviveOpen = false;


    void Awake()
    {
        instance = this;
    }

    public void WinGame(float performance)
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);

       
        int starsEarned = CalculateStars(performance);
      
        SaveProgress(starsEarned);

        UpdateWinUI(starsEarned);
    }

   
    private int CalculateStars(float score)
    {
        if (score >= 90) return 3; 
        if (score >= 50) return 2; 
        if (score >= 10) return 1;  
        return 0;
    }

    private void SaveProgress(int stars)
    {
        string starKey = "level_" + currentLevelID + "_stars";
        int oldStars = PlayerPrefs.GetInt(starKey, 0);

        
        if (stars > oldStars)
        {
            PlayerPrefs.SetInt(starKey, stars);
        }

        int reached = PlayerPrefs.GetInt("levelReached", 1);
        if (currentLevelID >= reached)
        {
            PlayerPrefs.SetInt("levelReached", currentLevelID + 1);
        }

        PlayerPrefs.Save();
    }

    private void UpdateWinUI(int stars)
    {
        // Desliga todas as estrelas antes de ligar as novas
        foreach (GameObject star in starImages) star.SetActive(false);

        for (int i = 0; i < stars; i++)
        {
            if (i < starImages.Length)
                starImages[i].SetActive(true);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.Log("Última fase concluída!");
            SceneManager.LoadScene("Menu");
        }
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