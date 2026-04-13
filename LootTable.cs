using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLootTable", menuName = "Loot System/Loot Table")]
public class LootTable : ScriptableObject
{
    [Header("Guaranteed Drops")]
    [Tooltip("Items in this list will ALWAYS drop when this table is rolled, ignoring any weights.")]
    public List<GameObject> guaranteedDrops = new List<GameObject>();

    [Header("Weighted Loot Configuration")]
    public List<LootItem> possibleDrops = new List<LootItem>();

    [Tooltip("If checked, there is a chance the enemy drops absolutely nothing from the weighted list.")]
    public bool canDropEmpty = false;
    [Tooltip("The weight of dropping nothing. Only applies if canDropEmpty is true.")]
    public int emptyDropWeight = 50;

    
    /// Returns a list of all items that should spawn (All Guaranteed Items + 1 Weighted Item).
    
    public List<GameObject> GenerateDrops()
    {
        List<GameObject> finalDrops = new List<GameObject>();

        // 1. Add all guaranteed drops to our final spawn list
        foreach (GameObject guaranteedItem in guaranteedDrops)
        {
            if (guaranteedItem != null)
            {
                finalDrops.Add(guaranteedItem);
            }
        }

        // 2. Calculate the random weighted drop and add it to the list
        GameObject weightedDrop = GetRandomDrop();
        if (weightedDrop != null)
        {
            finalDrops.Add(weightedDrop);
        }

        return finalDrops;
    }

    
    /// Calculates the total weight and returns a single random item based on their individual weights.
    
    private GameObject GetRandomDrop()
    {
        if (possibleDrops.Count == 0) return null;

        // 1. Calculate the total weight of all items combined
        int totalWeight = 0;
        foreach (LootItem item in possibleDrops)
        {
            totalWeight += item.weight;
        }

        if (canDropEmpty)
        {
            totalWeight += emptyDropWeight;
        }

        // 2. Pick a random number between 0 and the total weight
        int randomValue = Random.Range(0, totalWeight);

        // 3. Loop through the items and subtract their weight until we hit 0
        foreach (LootItem item in possibleDrops)
        {
            randomValue -= item.weight;
            
            // The item that pushes the random value below 0 is our winner!
            if (randomValue < 0)
            {
                return item.dropPrefab;
            }
        }

        // If the random value was high enough to skip all items, we hit the "Empty Drop"
        return null;
    }
}
