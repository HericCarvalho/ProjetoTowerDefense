using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        SaveSystem.SetLastPlayedLevel(SaveContext.currentSlot, currentLevel);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneLoader.instance.LoadScene("LevelSelection");
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
            SceneManager.LoadScene("Menu");
        }
    }

    public void UnlockNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int reachedLevel = PlayerPrefs.GetInt(SaveContext.GetKey("levelReached"), 1);

        if (currentLevel >= reachedLevel)
        {
            PlayerPrefs.SetInt(SaveContext.GetKey("levelReached"), currentLevel + 1);
            PlayerPrefs.Save();
        }
    }
}