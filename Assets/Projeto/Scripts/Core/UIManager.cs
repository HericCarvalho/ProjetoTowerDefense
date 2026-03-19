using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Slider healthBar;

    void Awake()
    {
        instance = this;
    }

    public void UpdateHealth(int value)
    {
        healthBar.value = value;
    }
}