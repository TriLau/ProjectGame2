using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    private Item _item;
    private int _quantity;
    private int _maxStack;
    private int _slotIndex;

    public Item Item
    { get { return _item; } }

    public int Quantity
    { get { return _quantity; } }

    public int MaxStack
    { get { return _maxStack; } }

    public int SlotIndex
    {  get { return _slotIndex; } }

    public InventoryItem(Item data, int index)
    {
        this._item = data;
        this._quantity ++;
        this._maxStack = 12;
        this._slotIndex = index;
    }

    public void AddQuantity(int amount)
    {
        _quantity += amount;
    }

    public void UpdateSlotIndex(int newIndex)
    {
        _slotIndex = newIndex;
    }
}
