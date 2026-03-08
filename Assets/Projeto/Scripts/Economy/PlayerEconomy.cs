using UnityEngine;

public class PlayerEconomy : MonoBehaviour
{
    public static PlayerEconomy instance;

    [Header("Resources")]
    public int money = 100;
    public int restos = 0;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one PlayerEconomy!");
            return;
        }

        instance = this;
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount)
            return false;

        money -= amount;
        return true;
    }

    public void AddRestos(int amount)
    {
        restos += amount;
    }

    public bool SpendRestos(int amount)
    {
        if (restos < amount)
            return false;

        restos -= amount;
        return true;
    }
}