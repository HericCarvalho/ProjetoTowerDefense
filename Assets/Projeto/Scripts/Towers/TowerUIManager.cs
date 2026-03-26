using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUIManager : MonoBehaviour
{
    public static TowerUIManager instance;

    public GameObject panel;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI pointsText;

    public Button damageButton;
    public Button rangeButton;
    public Button fireRateButton;

    public Button evolveButton;

    public GameObject transmutePanel;
    public Button transmuteA;
    public Button transmuteB;

    private Tower currentTower;

    void Awake()
    {
        instance = this;
        panel.SetActive(false);
        transmutePanel.SetActive(false);
    }

    public void SelectTower(Tower tower)
    {
        if (currentTower != null)
            currentTower.HideRange();

        currentTower = tower;

        panel.SetActive(true);
        UpdateUI();

        currentTower.ShowRange();
    }

    void Update()
    {
        if (currentTower == null) return;
        UpdateUI();
    }

    void UpdateUI()
    {
        levelText.text = "Lv " + currentTower.level;
        pointsText.text = "Points: " + currentTower.upgradePoints;

        bool canUpgrade = currentTower.upgradePoints > 0;

        damageButton.interactable = canUpgrade;
        rangeButton.interactable = canUpgrade;
        fireRateButton.interactable = canUpgrade;

        evolveButton.interactable = currentTower.level >= 5;

        if (currentTower.level >= 10)
            transmutePanel.SetActive(true);
    }

    public void UpgradeDamage()
    {
        currentTower.UpgradeDamage(10f);
    }

    public void UpgradeRange()
    {
        currentTower.UpgradeRange(1f);
    }

    public void UpgradeFireRate()
    {
        currentTower.UpgradeFireRate(0.2f);
    }

    public void Evolve()
    {
        currentTower.TryEvolve();
        Close();
    }

    public void OpenTransmute()
    {
        TransmuteUI.instance.Open(currentTower);
    }

    public void Close()
    {
        if (currentTower != null)
            currentTower.HideRange();

        panel.SetActive(false);
        currentTower = null;
    }
}