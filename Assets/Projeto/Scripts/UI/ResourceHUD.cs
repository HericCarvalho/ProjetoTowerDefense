using UnityEngine;
using TMPro;

public class ResourceHUD : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI scrapText;


    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        moneyText.text = "$ " + PlayerResources.instance.money;
        scrapText.text = "* " + PlayerResources.instance.restos;
    }
}