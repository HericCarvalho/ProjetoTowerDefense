using UnityEngine;
using UnityEngine.EventSystems;

public class ReviveManager : MonoBehaviour
{
    public static ReviveManager instance;

    void Awake()
    {
        instance = this;
    }

    public void Revive(string enemyID, GameObject prefab, int manaCost)
    {
        if (!FragmentManager.instance.CanRevive(enemyID))
        {
            Debug.Log("Fragmentos insuficientes");
            return;
        }

        if (!PlayerMana.instance.HasMana(manaCost))
        {
            Debug.Log("Mana insuficiente");
            return;
        }

        PlayerMana.instance.SpendMana(manaCost);
        FragmentManager.instance.ConsumeFragments(enemyID);

        GameObject enemy = ObjectPool.instance.GetObject(prefab);

        enemy.transform.position = GetSpawnPosition();
    }

    Vector3 GetSpawnPosition()
    {
        return Vector3.zero; 
    }
}