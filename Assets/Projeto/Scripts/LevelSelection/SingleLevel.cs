using UnityEngine;
using UnityEngine.SceneManagement;

public class SingleLevel : MonoBehaviour
{
     
    public void BackButton(string _LevelName)
    {
        SceneManager.LoadScene(_LevelName);
    }

}
