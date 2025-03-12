using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropData
{
    public int currentStage;
    public int growthTimeLeft;
    public bool isWatered;
    public int timeToChangeStage;
    public int stageTimeCounter;
    public bool needChangeStage;
    public TileBase[] growthStages;

    public CropData(int growthTime, TileBase[] growthStages)
    {
        needChangeStage = false;
        currentStage = 0;
        isWatered = false;
        growthTimeLeft = growthTime;
        this.growthStages = growthStages;
        timeToChangeStage = growthTime / growthStages.Length;
        stageTimeCounter = 0;
        
    }

    public void GrowthTimeUpdate(int minute)
    {
        growthTimeLeft -= minute;
        stageTimeCounter += minute;

        if(stageTimeCounter >= timeToChangeStage && currentStage < growthStages.Length - 1)
        {
            needChangeStage = true;
            currentStage++;
            stageTimeCounter = 0;
        }
    }
    public bool IsFullyGrown()
    {
        return currentStage == growthStages.Length - 1;
    }
}
