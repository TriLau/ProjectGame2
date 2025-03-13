using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class CropData
{
    [SerializeField] public int currentStage;
    [SerializeField] public int growthTimeLeft;
    [SerializeField] public bool isWatered;
    [SerializeField] public int timeToChangeStage;
    [SerializeField] public int stageTimeCounter;
    [SerializeField] public bool needChangeStage;
    [SerializeField] public ESeason season;
    [SerializeField] public TileBase[] growthStages;

    public CropData(int growthTime, TileBase[] growthStages, ESeason season)
    {
        needChangeStage = false;
        currentStage = 0;
        isWatered = false;
        growthTimeLeft = growthTime;
        this.growthStages = growthStages;
        timeToChangeStage = growthTime / growthStages.Length;
        stageTimeCounter = 0;
        this.season = season;
        
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
