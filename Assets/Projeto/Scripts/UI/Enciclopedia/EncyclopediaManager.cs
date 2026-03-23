using System.Collections.Generic;
using UnityEngine;

public class EncyclopediaManager : MonoBehaviour
{
    public static EncyclopediaManager instance;

    private HashSet<string> discovered = new HashSet<string>();
    private Dictionary<string, int> killCount = new Dictionary<string, int>();

    void Awake()
    {
        instance = this;
    }

    public void Discover(string id)
    {
        if (!discovered.Contains(id))
        {
            discovered.Add(id);
        }
    }

    public bool IsDiscovered(string id)
    {
        return discovered.Contains(id);
    }

    public void RegisterKill(string id)
    {
        if (!killCount.ContainsKey(id))
            killCount[id] = 0;

        killCount[id]++;
    }

    public int GetKills(string id)
    {
        return killCount.ContainsKey(id) ? killCount[id] : 0;
    }
}