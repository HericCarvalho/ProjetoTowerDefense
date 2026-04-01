using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleLevel : MonoBehaviour
{
     
    public void BackButton()
    {
        SceneManager.LoadScene("LevelSelection");
    }

}
