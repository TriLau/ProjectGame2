using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeField] private int _maxSlotInventory;
    [SerializeField] private List<InventoryItem> _ineventoryItemList;

    public int MaxSlotInventory
    {  get { return _maxSlotInventory; } }

    public List<InventoryItem> InventoryItemList
    {  get { return _ineventoryItemList; } }

    public Inventory() 
    {
        this._maxSlotInventory = 14;
        this._ineventoryItemList = new List<InventoryItem>(_maxSlotInventory);
    }

    public InventoryItem GetInventoryItemOfIndex(int index)
    {
        InventoryItem item = _ineventoryItemList.Find(i => i.SlotIndex == index);
        return item;
    }

    public bool AddItemToInventory(Item item, int slotIndex)
    {
        InventoryItem inventoryItem = new InventoryItem(item, slotIndex);
        _ineventoryItemList.Add(inventoryItem);
        return true;
    }

    public bool AddItemToInventory(Item item, int slotIndex, int amount)
    {
        InventoryItem inventoryItem = new InventoryItem(item, slotIndex, amount);
        _ineventoryItemList.Add(inventoryItem);
        return true;
    }

    public bool RemoveItemFromInventory(Item item, int amount = 1)
    {
        InventoryItem existingItem = _ineventoryItemList.Find(i => i.Item == item);
        if (existingItem != null)
        {
            existingItem.AddQuantity(-amount);
            if (existingItem.Quantity <= 0) _ineventoryItemList.Remove(existingItem); return true;
        }
        return false;
    }
}
