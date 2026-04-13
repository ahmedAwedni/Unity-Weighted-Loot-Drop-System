//3. LootSpawner.cs
using System.Collections.Generic;
using UnityEngine;

/// Attach this to an Enemy, Chest, or destructible barrel.

public class LootSpawner : MonoBehaviour
{
    [Header("Loot Settings")]
    public LootTable myLootTable;
    
    [Tooltip("Where should the item spawn? Leave empty to spawn at this object's center.")]
    public Transform spawnPoint;

    [Tooltip("How much random force to apply to the dropped item (makes loot 'pop' out).")]
    public float dropForce = 3f;

    public void DropLoot()
    {
        if (myLootTable == null)
        {
            Debug.LogWarning("No Loot Table assigned to " + gameObject.name);
            return;
        }

        // Ask the ScriptableObject to process the math and give us all winning items
        List<GameObject> itemsToSpawn = myLootTable.GenerateDrops();

        if (itemsToSpawn.Count > 0)
        {
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;
            
            // Loop through our list and spawn every item (Guaranteed + Weighted)
            foreach (GameObject itemPrefab in itemsToSpawn)
            {
                GameObject spawnedLoot = Instantiate(itemPrefab, spawnPos, Quaternion.identity);

                // Optional: Add a little physics "pop" so the loot flies out
                Rigidbody rb = spawnedLoot.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
                    rb.AddForce(randomDirection * dropForce, ForceMode.Impulse);
                }
            }
        }
        else
        {
            Debug.Log(gameObject.name + " dropped nothing.");
        }
    }
}
