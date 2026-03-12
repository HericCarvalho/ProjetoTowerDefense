using UnityEngine;

public class TowerXP : MonoBehaviour
{
    public int level = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 10;

    public int evolutionStage = 1;

    public void GainXP(int amount)
    {
        currentXP += amount;

        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentXP -= xpToNextLevel;

        level++;

        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.5f);

        Debug.Log("Tower Level Up: " + level);

        CheckEvolution();
    }

    void CheckEvolution()
    {
        if (level % 5 == 0)
        {
            Debug.Log("Evolution Available!");
        }
    }
}