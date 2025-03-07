using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObject/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<Item> items;

    public Item GetItemByName(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }
}
