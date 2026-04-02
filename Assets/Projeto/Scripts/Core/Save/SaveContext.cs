public static class SaveContext
{
    public static int currentSlot = 1;

    public static string GetKey(string key)
    {
        return $"SLOT_{currentSlot}_{key}";
    }
}