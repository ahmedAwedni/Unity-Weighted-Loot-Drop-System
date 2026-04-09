// 1. LootItem.cs
using UnityEngine;


/// A serializable class to hold the data for a single possible drop.

[System.Serializable]
public class LootItem
{
    public string itemName = "New Item";
    [Tooltip("The actual physical object that will spawn in the world.")]
    public GameObject dropPrefab;
    
    [Tooltip("Higher weight = higher chance to drop. (e.g., Common = 100, Rare = 10, Legendary = 1)")]
    [Min(0)] public int weight = 10;
}