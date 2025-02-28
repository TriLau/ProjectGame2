using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    private Item _item;
    private int _count;

    public Item Item
    { get { return _item; } }

    public int Count
    { get { return _count; } }

    public InventorySlot()
    {
        _item = new Item();
        _count = 0;
    }

    public void AddItemToSlot(Item item)
    {
        this._item = item;
    }
}
