using UnityEngine;
using UnityEngine.UI;

public class UnitButtonUI : MonoBehaviour
{
    public GameObject unitPrefab;
    public Text amountText;

    void Update()
    {
        if (FragmentManager.instance == null)
            return;

        int amount = FragmentManager.instance.GetAmount(unitPrefab);

        amountText.text = amount.ToString();

        GetComponent<Button>().interactable = amount > 0;
    }

    public void OnClick()
    {
        UnitPlacementInput.instance.SetSelectedUnit(unitPrefab);

        GameManager.instance.CloseReviveHUD();
    }
}