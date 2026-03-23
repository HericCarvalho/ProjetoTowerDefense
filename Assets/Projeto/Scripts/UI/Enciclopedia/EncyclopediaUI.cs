using UnityEngine;
using System.Collections.Generic;

public class EncyclopediaUI : MonoBehaviour
{
    public static EncyclopediaUI instance;
    public EnemyDetailUI detailUI;

    [Header("Database")]
    public List<EnemyData> allEnemies;

    [Header("UI")]
    public GameObject mainPanel;
    public GameObject PainelButtons;
    public GameObject monstropediaPanel;
    public Transform contentParent;
    public GameObject entryPrefab;

    void Awake()
    {
        instance = this;
    }

    public void OpenEnemies()
    {
        monstropediaPanel.SetActive(true);
        PainelButtons.SetActive(false);

        foreach (var enemy in allEnemies)
        {
            if (!EncyclopediaManager.instance.IsDiscovered(enemy.enemyID))
                continue;

            GameObject go = Instantiate(entryPrefab, contentParent);

            EnemyEntryUI entry = go.GetComponent<EnemyEntryUI>();

            entry.Setup(enemy, detailUI);
        }
    }
    public void OpenTowers()
    {
        Debug.Log("Abrir torres (em breve)");
    }

    void Clear()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void ShowDetails(EnemyData data)
    {
        Debug.Log("Mostrar detalhes: " + data.enemyName);
        int kills = EncyclopediaManager.instance.GetKills(data.enemyID);

        if (kills >= 5)
            Debug.Log("HP: " + data.maxHealth);

        if (kills >= 10)
            Debug.Log("Dano: " + data.damage);
    }
    public void OpenMainPanel()
    {
        mainPanel.SetActive(true);
    }
}