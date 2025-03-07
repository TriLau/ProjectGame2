using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropData
{
    public int growthStage;
    public int dayPlanted;
    public int growthTime;
    public TileBase[] growthStages;

    public bool IsFullyGrown(float currentTime)
    {
        return currentTime >= dayPlanted + growthTime;
    }
}
