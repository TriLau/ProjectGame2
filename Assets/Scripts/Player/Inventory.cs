using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    private int _maxSlotInventory;
    private List<InventoryItem> _ineventoryItemList;

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
        return _ineventoryItemList[index];
    }

    public InventoryItem AddItemToInventory(Item item, int slotIndex)
    {
        InventoryItem inventoryItem = new InventoryItem(item, slotIndex);
        _ineventoryItemList.Add(inventoryItem);
        return inventoryItem;
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

    public void SortedSlotList()
    {
        _ineventoryItemList.Sort((x, y) => x.Item.itemName.CompareTo(y.Item.itemName));
    }
}
