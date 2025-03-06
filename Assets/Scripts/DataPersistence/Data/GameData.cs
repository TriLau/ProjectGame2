using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [SerializeField] private Player _playerData;
    [SerializeField] private Inventory _inventoryData;
    [SerializeField] private Season _seasonData;

    public Player PlayerData
    { get { return _playerData; } }

    public Inventory InventoryData
    { get { return _inventoryData; } }

    public Season SeasonData
    { get { return _seasonData; } }

    public GameData()
    {
        this._playerData = new Player();
        this._inventoryData = new Inventory();
        this._seasonData = new Season();
    }

    public void SetPlayerData(Player playerData)
    {
        this._playerData = playerData;
    }

    public void SetInventoryData(Inventory inventoryData)
    {
        this._inventoryData = inventoryData;

        foreach (var item in inventoryData.InventoryItemList)
        {
            Debug.Log($"i[{item.SlotIndex}]: {item.Item.itemName} - {item.Quantity}");
        }
    }

    public void SetSeason(Season season)
    { 
        this._seasonData = season; 
    }
}
