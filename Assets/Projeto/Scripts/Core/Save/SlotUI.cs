using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour
{
    public int slotId;

    [Header("UI")]
    public GameObject newGameText;
    public GameObject continueText;
    public GameObject deleteButton;

    [Header("Preview UI")]
    public TextMeshProUGUI starsText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI levelNameText;

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        bool hasSave = SaveSystem.HasSave(slotId);

        newGameText.SetActive(!hasSave);
        continueText.SetActive(hasSave);
        deleteButton.SetActive(hasSave);

        if (hasSave)
        {
            var preview = SaveSystem.GetSlotPreview(slotId);

            starsText.text = $"* {preview.totalStars}";
            levelText.text = $"Fase {preview.lastPlayedLevel}";

            string levelName = LevelDatabase.GetLevelName(preview.lastPlayedLevel);
            levelNameText.text = levelName;
        }
        else
        {
            starsText.text = "";
            levelText.text = "";
            levelNameText.text = "";
        }
    }

    public void OnClickSlot()
    {
        SaveContext.currentSlot = slotId;
        SceneManager.LoadScene("LevelSelection");
    }
    public void OnDeleteSlot()
    {
        SaveSystem.DeleteSlot(slotId);
        Refresh();
    }
    //public void OnClickSlotContinue()
    //{
    //    SaveContext.currentSlot = slotId;
    //
    //    int lastLevel = SaveSystem.GetLastPlayedLevel(slotId);
    //
    //    SceneManager.LoadScene(lastLevel);
    //}
}