using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ListItemWorld
{
    [SerializeField] private SerializableDictionary<string, ItemWorld> _items;
    
    public Dictionary<string, ItemWorld> Items
    { get { return _items; } }

    public ListItemWorld() 
    {
        _items = new SerializableDictionary<string, ItemWorld>();
    }

    public void AddItemWorld(ItemWorld item)
    {
        _items.Add(item.Id, item);
    }

    public void RemoveItemWorld(ItemWorld item)
    {
        _items.Remove(item.Id);
    }
}
