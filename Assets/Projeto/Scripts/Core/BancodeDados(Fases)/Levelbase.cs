using System.Collections.Generic;
using UnityEngine;

public class Levelbase : MonoBehaviour
{
    public static Levelbase instance;

    public List<LevelData> levels;

    void Awake()
    {
        instance = this;
    }

    public LevelData GetLevel(string sceneName)
    {
        return levels.Find(l => l.sceneName == sceneName);
    }
}