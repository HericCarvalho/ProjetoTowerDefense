using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransmuteUI : MonoBehaviour
{
    public static TransmuteUI instance;

    public GameObject panel;

    [Header("Option A")]
    public Image imageA;
    public TextMeshProUGUI nameA;
    public Button buttonA;

    [Header("Option B")]
    public Image imageB;
    public TextMeshProUGUI nameB;
    public Button buttonB;

    private Tower currentTower;

    void Awake()
    {
        instance = this;
        panel.SetActive(false);
    }

    public void Open(Tower tower)
    {
        currentTower = tower;

        TowerData a = tower.data.transmuteOptionA;
        TowerData b = tower.data.transmuteOptionB;

        if (a == null || b == null)
        {
            Debug.LogError("Transmute options not set!");
            return;
        }

        imageA.sprite = a.icon;
        nameA.text = a.towerName;

        imageB.sprite = b.icon;
        nameB.text = b.towerName;

        buttonA.onClick.RemoveAllListeners();
        buttonB.onClick.RemoveAllListeners();

        buttonA.onClick.AddListener(() => SelectOption(a));
        buttonB.onClick.AddListener(() => SelectOption(b));

        panel.SetActive(true);
    }

    void SelectOption(TowerData option)
    {
        currentTower.Transmute(option);
        Close();
    }

    public void Close()
    {
        panel.SetActive(false);
        currentTower = null;
    }
}