# Unity Weighted Loot Drop System

A highly flexible, data-driven Loot Table system for Unity. Built using **ScriptableObjects** and a proportional weight algorithm, this system allows designers to easily balance item drop chances without ever having to calculate percentages manually. 

---

## ✨ Features

* **Proportional Weighting:** Assign arbitrary weights to items (e.g., Gold = 100, Sword = 10, Legendary Axe = 1). The system dynamically calculates the exact odds, meaning you can add or remove items without breaking the math.
* **Guaranteed Drops:** Includes a dedicated list for items that bypass the math entirely and will *always* drop when the table is rolled. Perfect for Boss keys, quest items, or fixed rewards.
* **ScriptableObject Tables:** Create specific Loot Tables for different enemies or areas (e.g., "GoblinLootTable", "BossChestLootTable") entirely in the Unity Editor.
* **Empty Drop Support:** Built-in support for "Empty Drops". You can easily configure a table so that a basic enemy only drops a randomized item 20% of the time, and drops nothing the other 80%.
* **Physics Integration:** The included Spawner script automatically detects "Rigidbodies" on dropped items and applies a randomized explosive force so loot "pops" out of chests and enemies beautifully.

---

## 🧠 Design Notes

**Why Weights instead of Percentages?**
If you build a system using strict percentages (Gold 50%, Potion 30%, Sword 20% = 100%), what happens when you want to add a Shield? You have to manually reduce the percentages of the other three items to make room for it. This makes balancing an RPG a nightmare.

With a **Weighted Algorithm**, the total is dynamic. The system adds up all the numbers (100 + 10 + 1 = Total 111). It rolls a random number between 0 and 111, and subtracts weights down the list until it hits zero. This means adding a new item is as simple as dropping it in the list and giving it a weight. The math balances itself.

---

## 📂 Included Scripts

* "LootItem.cs" - A serializable data class defining the name, the physical prefab to spawn, and the numerical weight of a specific drop.
* "LootTable.cs" - The ScriptableObject blueprint that holds a list of "LootItems", a list of guaranteed drops, and contains the mathematical algorithm for generating the final loot pool.
* "LootSpawner.cs" - A MonoBehaviour component you can attach to an enemy or chest. When triggered, it spawns all winning prefabs into the world simultaneously.

---

## 🧩 How To Use

1. **Create a Loot Table:** Right-click in your Project window: "Create > Loot System > Loot Table". Name it something like "SlimeLoot".
2. **Add Guaranteed Items (Optional):** Drag any GameObjects that *must* drop (like a Boss Key) into the "Guaranteed Drops" array.
3. **Add Weighted Items:** Add a few elements to the "Weighted Loot Configuration" list. Assign your 3D/2D item prefabs and give them weights (e.g., SlimeGoo = 100, RustyDagger = 5).
4. **Setup the Spawner:** Attach "LootSpawner.cs" to your enemy prefab in the scene. Drag your "SlimeLoot" ScriptableObject into the table reference slot.
5. **Trigger the Drop:** Call the drop method from your enemy's health or death script:

"""

public LootSpawner myLootSpawner;

void Die()
{
    myLootSpawner.DropLoot();
    Destroy(gameObject);
}

"""

---

## 🚀 Possible Extensions

* **Pool System Integration:** Instead of using "Instantiate(itemPrefab)", link this system to a **Unity Object Pooling System** to spawn hundreds of coins and items without impacting garbage collection!
* **Conditional Drops:** Add a system that checks the player's level or current active quests before adding certain items into the pool.

---

## 🛠 Unity Version

* Tested in Unity6+ (should work without any problems in newer versions)

---

## 📜 License

MIT
