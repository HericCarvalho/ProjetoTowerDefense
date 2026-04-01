using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked; //default value is false;
    public Image unlockImage;
    public GameObject[] stars;


    private void Update()
    {
        UpdateLevelImage();
    }
    private void UpdateLevelImage()
    {
        if (!unlocked)
        {
            unlockImage.gameObject.SetActive(true);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(false);
            }
        }
        else
        {
            unlockImage.gameObject.SetActive(false);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(true);
            }
        }
    }

    public void PressSelection(string _LevelName)
    {
        if (unlocked == true)
        {
            SceneManager.LoadScene(_LevelName);
        }
    }

}
