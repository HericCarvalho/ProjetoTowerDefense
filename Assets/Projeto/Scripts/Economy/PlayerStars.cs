using UnityEngine;

public class PlayerStars : MonoBehaviour
{
    public static PlayerStars instance;

    public int totalStars;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void AddStars(int amount)
    {
        totalStars += amount;
        Save();
    }

    public bool CanSpend(int amount)
    {
        return totalStars >= amount;
    }

    public void Spend(int amount)
    {
        if (!CanSpend(amount)) return;

        totalStars -= amount;
        Save();
    }

    void Save()
    {
        PlayerPrefs.SetInt("TOTAL_STARS", totalStars);
        PlayerPrefs.Save();
    }

    void Load()
    {
        totalStars = PlayerPrefs.GetInt("TOTAL_STARS", 0);
    }
}