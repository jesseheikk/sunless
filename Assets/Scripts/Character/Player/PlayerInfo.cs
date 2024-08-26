using System.Collections.Generic;
using UnityEngine;

public static class PlayerInfo
{
    public static int coins { get; set; } = 0;
    public static float maxHealth { get; set; } = 100f;
    public static float currentHealth { get; set; } = 100f;
    public static float attack { get; set; } = 20f;

    public static string spawnPoint { get; set; } = "";
    public static Dictionary<string, int> npcDialogStates = new Dictionary<string, int>();
    public static List<string> collectedItems = new List<string>();

    public static void AddCollectedItem(string itemId)
    {
        if (!collectedItems.Contains(itemId))
        {
            collectedItems.Add(itemId);
        }
    }

    public static bool HasCollectedItem(string itemId)
    {
        return collectedItems.Contains(itemId);
    }
}