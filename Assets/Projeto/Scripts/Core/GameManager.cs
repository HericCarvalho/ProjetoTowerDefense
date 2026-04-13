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