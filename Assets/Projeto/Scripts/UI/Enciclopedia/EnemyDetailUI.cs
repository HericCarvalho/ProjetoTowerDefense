using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDetailUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;

    public Image icon;
    public TextMeshProUGUI nameText;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI physResText;
    public TextMeshProUGUI magicResText;
    public TextMeshProUGUI immunityText;
    public TextMeshProUGUI descriptionText;

    [Header("Unlock Requirements")]
    public int hpUnlock = 5;
    public int damageUnlock = 10;
    public int speedUnlock = 15;
    public int attackSpeedUnlock = 12;
    public int rangeUnlock = 15;
    public int resistanceUnlock = 18;
    public int immunityUnlock = 25;
    public int descriptionUnlock = 20;

    public void Show(EnemyData data)
    {
        panel.SetActive(true);

        int kills = EncyclopediaManager.instance.GetKills(data.enemyID);

        // Sempre visível
        nameText.text = data.enemyName;
        icon.sprite = data.icon;

        // HP
        if (kills >= hpUnlock)
            hpText.text = "HP: " + data.maxHealth;
        else
            hpText.text = "HP: ???";

        // Damage
        if (kills >= damageUnlock)
            damageText.text = "Dano: " + data.damage;
        else
            damageText.text = "Dano: ???";

        // Speed
        if (kills >= speedUnlock)
            speedText.text = "Velocidade: " + data.speed;
        else
            speedText.text = "Velocidade: ???";
        // Attack Speed
        if (kills >= attackSpeedUnlock)
            attackSpeedText.text = "Atk Speed: " + data.attackSpeed;
        else
            attackSpeedText.text = "Atk Speed: ???";

        // Range
        if (kills >= rangeUnlock)
            rangeText.text = "Range: " + data.attackRange;
        else
            rangeText.text = "Range: ???";

        // Resistances
        if (kills >= resistanceUnlock)
        {
            physResText.text = "Físico: " + (data.physicalResistance * 100f).ToString("F0") + "%";
            magicResText.text = "Mágico: " + (data.physicalResistance * 100f).ToString("F0") + "%";
        }
        else
        {
            physResText.text = "Físico: ???";
            magicResText.text = "Mágico: ???";
        }

        // Immunities
        if (kills >= immunityUnlock)
        {
            string immunities = "";

            if (data.immuneToBurn) immunities += "Burn ";
            if (data.immuneToSlow) immunities += "Slow ";
            if (data.immuneToStun) immunities += "Stun ";

            immunityText.text = immunities == "" ? "Nenhuma" : immunities;
        }
        else
        {
            immunityText.text = "???";
        }
        // Description
        if (kills >= descriptionUnlock)
            descriptionText.text = data.description;
        else
            descriptionText.text = "Derrote mais inimigos para desbloquear...";
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}