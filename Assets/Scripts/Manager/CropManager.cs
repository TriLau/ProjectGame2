
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropManager : Singleton<CropManager>
{
    public Tilemap cropTilemap;

    private Dictionary<Vector3Int, CropData> _plantedCrops = new Dictionary<Vector3Int, CropData>();
    public Dictionary<Vector3Int, CropData> PlantedCrops
    {
        get { return _plantedCrops; }
        private set { _plantedCrops = value; }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlantCrop(bool isFarmGround, Vector3Int plantPosition, Item crop)
    {
        if (isFarmGround && !_plantedCrops.ContainsKey(plantPosition))
        {
            CropData newCrop = new CropData(crop.TimeToGrowth, crop.growthStages);
            
            _plantedCrops.Add(plantPosition, newCrop);
            cropTilemap.SetTile(plantPosition, newCrop.growthStages[0]);
            Debug.Log($"add crop tile at {plantPosition}");
        }
    }

    public void UpdateCropsGrowthTime(int minute)
    {
        foreach(var crop in _plantedCrops.ToList())
        {
            var cropInfo = crop.Value;
            if (TileManager.Instance.WateredTiles.ContainsKey(crop.Key))
                cropInfo.isWatered = true;
            else cropInfo.isWatered = false;
            if (!cropInfo.IsFullyGrown())
            {
                if (cropInfo.isWatered)
                {
                    cropInfo.GrowthTimeUpdate(minute);
                }
                if (cropInfo.needChangeStage)
                {
                    cropInfo.needChangeStage = false;
                    cropTilemap.SetTile(crop.Key, cropInfo.growthStages[cropInfo.currentStage]);
                }
            }
            else
            {
                Debug.Log("crop fully grew");
            }
            
        }
    }
}
