using System.Collections.Generic;
using UnityEngine;

public class FragmentManager : MonoBehaviour
{
    public static FragmentManager instance;

    private Dictionary<GameObject, int> fragments = new Dictionary<GameObject, int>();

    void Awake()
    {
        instance = this;
    }

    public void AddFragment(GameObject fragment)
    {
        if (!fragments.ContainsKey(fragment))
            fragments[fragment] = 0;

        fragments[fragment]++;
    }

    public bool HasFragment(GameObject fragment)
    {
        return fragments.ContainsKey(fragment) && fragments[fragment] > 0;
    }

    public void UseFragment(GameObject fragment)
    {
        if (HasFragment(fragment))
            fragments[fragment]--;
    }

    public int GetAmount(GameObject fragment)
    {
        return fragments.ContainsKey(fragment) ? fragments[fragment] : 0;
    }
}