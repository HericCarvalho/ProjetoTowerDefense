using UnityEngine;

public class PauseController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject pausePanel;

    private bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (isPaused) return;

        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        if (!isPaused) return;

        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }
}