using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyEntryUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI nameText;

    private EnemyData data;

    private EnemyDetailUI detailUI;

    public void Setup(EnemyData d, EnemyDetailUI ui)
    {
        data = d;
        detailUI = ui;

        icon.sprite = d.icon;
        nameText.text = d.enemyName;
    }

    public void OnClick()
    {
        if (data == null)
        {
            Debug.LogError("EnemyData NULL no Entry!");
            return;
        }

        if (detailUI == null)
        {
            Debug.LogError("EnemyDetailUI NULL!");
            return;
        }

        detailUI.Show(data);
    }
}