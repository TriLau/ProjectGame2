using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateredTileData
{
    public int timeToRemoveTile;
    public int removeTileCounter;
    public bool needRemove;

    public WateredTileData()
    {
        timeToRemoveTile = 1440;
        removeTileCounter = 0;
        needRemove = false;
    }

    public void CheckTile(int minute)
    {
        removeTileCounter += minute;
        if (removeTileCounter >= timeToRemoveTile)
        {
            needRemove = true;
        }
    }
}
