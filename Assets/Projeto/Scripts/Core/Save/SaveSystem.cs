using UnityEngine;

public static class SaveSystem
{
    public static bool HasSave(int slot)
    {
        return PlayerPrefs.HasKey($"SLOT_{slot}_lastPlayedLevel");
    }

    public static void DeleteSlot(int slot)
    {
        PlayerPrefs.DeleteKey($"SLOT_{slot}_TOTAL_STARS");
        PlayerPrefs.DeleteKey($"SLOT_{slot}_SKILLS");
        PlayerPrefs.DeleteKey($"SLOT_{slot}_levelReached");
        PlayerPrefs.DeleteKey($"SLOT_{slot}_lastPlayedLevel");

        for (int i = 0; i < 100; i++)
        {
            PlayerPrefs.DeleteKey($"SLOT_{slot}_level_{i}_stars");
        }

        PlayerPrefs.Save();

        Debug.Log($"[DELETE] Slot {slot} completamente apagado.");
    }
    public struct SlotPreview
    {
        public int totalStars;
        public int levelReached;
        public int lastPlayedLevel;
    }

    public static SlotPreview GetSlotPreview(int slot)
    {
        SlotPreview preview = new SlotPreview();

        preview.totalStars = PlayerPrefs.GetInt($"SLOT_{slot}_TOTAL_STARS", 0);
        preview.levelReached = PlayerPrefs.GetInt($"SLOT_{slot}_levelReached", 1);
        preview.lastPlayedLevel = PlayerPrefs.GetInt($"SLOT_{slot}_lastPlayedLevel", 1);

        return preview;
    }
    public static void SetLastPlayedLevel(int slot, int levelIndex)
    {
        PlayerPrefs.SetInt($"SLOT_{slot}_lastPlayedLevel", levelIndex);
        PlayerPrefs.Save();
    }
    public static int GetLastPlayedLevel(int slot)
    {
        if (!PlayerPrefs.HasKey($"SLOT_{slot}_lastPlayedLevel"))
            return -1;

        return PlayerPrefs.GetInt($"SLOT_{slot}_lastPlayedLevel");
    }
}