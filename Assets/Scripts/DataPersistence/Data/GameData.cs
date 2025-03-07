using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [SerializeField] private Player _playerData;
    [SerializeField] private Inventory _inventoryData;
    [SerializeField] private EnvironmentalStatus _eStatus;
    [SerializeField] private ListItemWorld _listItemWold;

    public Player PlayerData
    { get { return _playerData; } }

    public Inventory InventoryData
    { get { return _inventoryData; } }

    public EnvironmentalStatus EnviromentData
    { get { return _eStatus; } }

    public ListItemWorld ListItemWold
    { get { return _listItemWold; } }

    public GameData()
    {
        this._playerData = new Player();
        this._inventoryData = new Inventory();
        this._eStatus = new EnvironmentalStatus();
        this._listItemWold = new ListItemWorld();
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

    public void SetSeason(EnvironmentalStatus status)
    { 
        this._eStatus = status; 
    }

    public void SetListItemWorld(ListItemWorld itemWorld)
    {
        this._listItemWold = itemWorld;
    }
}
