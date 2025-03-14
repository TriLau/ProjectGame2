using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemWorld : IItemHolder
{
    [NonSerialized] private Item _item;
    [SerializeField] private string _id;
    [SerializeField] private string _itemName;
    [SerializeField] private int _quantity;
    [SerializeField] private Vector3 _position;
    [SerializeField] private bool _isColected;
   
    public Item Item
    { get { return _item; } }

    public string Id
    { get { return _id; } }

    public string ItemName
    { get { return _itemName; } }

    public int Quantity
    { get { return _quantity; } }

    public Vector3 Position
    { get { return _position; } }

    public bool IsColected
    { get { return _isColected; } }

    public ItemWorld()
    {
        this._id = string.Empty;
        this._item = null;
        this._quantity = 0;
        this._position = Vector3.zero;
        this._isColected = false;
    }

    public ItemWorld(string id, Item item, int quantity, Vector3 position)
    {
        _id = id;
        _item = item;
        _itemName = item.itemName;
        _quantity = quantity;
        _position = position;
        _isColected = false;
    }

    public void SetItem(Item item)
    {
        _item = item;
    }

    public void SetId()
    {
        _id = System.Guid.NewGuid().ToString();
    }

    public void SetColected(bool colected)
    {
        _isColected = colected;
    }
}
