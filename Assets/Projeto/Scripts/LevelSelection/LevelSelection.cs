using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [Header("Configurações")]
    public int levelID;
    public string levelName;

    [Header("Elementos de UI")]
    public GameObject lockImage;       
    public GameObject starsContainer;  
    public GameObject[] filledStars;  

    private bool isUnlocked;

    private void Start()
    {
        UpdateLevelStatus();
    }

    public void UpdateLevelStatus()
    {
        
        int reachedLevel = PlayerPrefs.GetInt("levelReached", 1);
        isUnlocked = (levelID <= reachedLevel);

        
        if (lockImage != null)
        {
            lockImage.SetActive(!isUnlocked);
        }

        
        if (!isUnlocked)
        {
            
            starsContainer.SetActive(false);
        }
        else
        {
            
            starsContainer.SetActive(true);

            
            int starsEarned = PlayerPrefs.GetInt("level_" + levelID + "_stars", 0);

          
            for (int i = 0; i < filledStars.Length; i++)
            {
                filledStars[i].SetActive(i < starsEarned);
            }
        }

        
        Button btn = GetComponent<Button>();
        if (btn != null) btn.interactable = isUnlocked;
    }

    public void PressSelection()
    {
        if (isUnlocked)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
        }
    }
}