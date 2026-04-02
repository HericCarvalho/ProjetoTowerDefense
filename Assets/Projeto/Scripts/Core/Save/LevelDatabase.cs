using UnityEngine;

public static class LevelDatabase
{
    public static string GetLevelName(int index)
    {
        switch (index)
        {
            case 1: return "Floresta Sombria";
            case 2: return "Deserto Escaldante";
            case 3: return "Montanha Congelada";
            default: return "Desconhecido";
        }
    }
}