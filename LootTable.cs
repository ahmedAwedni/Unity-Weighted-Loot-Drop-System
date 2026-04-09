// 2. LootTable.cs
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLootTable", menuName = "Loot System/Loot Table")]
public class LootTable : ScriptableObject
{
    [Header("Loot Configuration")]
    public List<LootItem> possibleDrops = new List<LootItem>();

    [Tooltip("If checked, there is a chance the enemy drops absolutely nothing.")]
    public bool canDropEmpty = false;
    [Tooltip("The weight of dropping nothing. Only applies if canDropEmpty is true.")]
    public int emptyDropWeight = 50;

  
    /// Calculates the total weight and returns a random item based on their individual weights.
  
    public GameObject GetRandomDrop()
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