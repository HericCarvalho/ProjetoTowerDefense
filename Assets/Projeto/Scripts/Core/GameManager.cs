using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject winPanel;
    public GameObject gameOverPanel;

    public GameObject reviveHUD;

    private bool isReviveOpen = false;


    void Awake()
    {
        instance = this;
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        winPanel.SetActive(true);
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