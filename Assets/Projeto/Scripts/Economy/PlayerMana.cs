using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public static PlayerMana instance;

    [Header("Mana")]
    public float maxMana = 100f;
    public float currentMana;
    public float manaRegen = 5f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentMana = maxMana;
    }

    void Update()
    {
        RegenerateMana();
    }

    void RegenerateMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaRegen * Time.deltaTime;

            if (currentMana > maxMana)
                currentMana = maxMana;
        }
    }

    public bool HasMana(float amount)
    {
        return currentMana >= amount;
    }

    public void SpendMana(float amount)
    {
        currentMana -= amount;

        if (currentMana < 0)
            currentMana = 0;
    }
}