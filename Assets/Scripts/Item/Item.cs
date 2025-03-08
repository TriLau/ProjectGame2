using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scripable object/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public TileBase tile;
    public RuleTile ruleTile;
    public AnimatedTile animatedTile;
    public Tilemap tilemap;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
    public string itemName;
}

public enum ItemType
{
    Tile,
    Food,
    Tool,
    Material,
    Crop
}

public enum ActionType
{
    Cook,
    Combine,
    Mine,
    Craft,
    PlaceTile
}
