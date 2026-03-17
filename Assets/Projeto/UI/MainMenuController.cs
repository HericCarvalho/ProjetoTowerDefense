using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string gameSceneName = "GameScene";

    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;


    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenOptions()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");

        Application.Quit();
    }
}