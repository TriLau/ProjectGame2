using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoedTileData
{
    public int timeToRemoveTile;
    public int removeTileCounter;
    public bool hasSomethingOn;
    public bool needRemove;

    public HoedTileData()
    {
        timeToRemoveTile = 4320;
        removeTileCounter = 0;
        hasSomethingOn = false;
        needRemove = false;
    }

    public void CheckTile(int minute)
    {
        if (!hasSomethingOn)
        {
            removeTileCounter += minute;
            if(removeTileCounter >= timeToRemoveTile)
            {
                needRemove = true;
            }
        }
        else
        {
            removeTileCounter = 0;
        }
    }
}
