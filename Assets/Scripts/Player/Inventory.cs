using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    private List<InventorySlot> _slotList;
    
    public List<InventorySlot> ItemList
    { get { return _slotList; } }

    public Inventory() 
    {
        _slotList = new List<InventorySlot>();
    }

    public Inventory(List<InventorySlot> slotList)
    {
        _slotList = slotList;
    }

    public void AddSlotToInventory(InventorySlot slot)
    {
        _slotList.Add(slot);
    }

    public void RemoveSlotFromInventory(InventorySlot item)
    {
        _slotList.Remove(item);
    }

    public void SortedSlotList()
    {
        _slotList.Sort((x, y) => x.Item.itemName.CompareTo(y.Item.itemName));
    }

    public void SwapItemInSlot(int index, Item item)
    {
        Print();
    }

    public void AddItemToInventorySlot(Item itemAdded)
    {
        foreach (InventorySlot slot in _slotList)
        {
            Item item = slot.Item;
            if (item == null) slot.AddItemToSlot(itemAdded);
            else if (item != null)
            {

            }
        }
    }

    public void Print()
    {
        foreach (InventorySlot slot in _slotList)
        {
            int index = _slotList.IndexOf(slot);
            Item item = slot.Item;
            if (item !=  null)
            {
                Debug.Log($"i: {index} - item: {item.itemName}\n");
            }
            else
            {
                Debug.Log($"i: {index} - item: null\n");
            }
        }
    }
}
