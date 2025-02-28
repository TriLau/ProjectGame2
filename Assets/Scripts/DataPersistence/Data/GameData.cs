using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    private Player _playerData;
    private Inventory _inventoryData;

    public Player PlayerData
    { get { return _playerData; } }

    public Inventory InventoryData
    { get { return _inventoryData; } }

    public GameData()
    {
        this._playerData = new Player();
        this._inventoryData = new Inventory();
    }

    public void SetPlayerData(Player playerData)
    {
        this._playerData = playerData;
    }

    public void SetInventoryData(Inventory inventoryData)
    {
        this._inventoryData = inventoryData;
    }
}
