using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : Singleton<TileManager>
{
    [SerializeField]
    private Tilemap _hoedTilemap;
    [SerializeField]
    private Tilemap _wateredTilemap;
    private Dictionary<Vector3Int, HoedTileData> _hoedTiles = new Dictionary<Vector3Int, HoedTileData>();
    public Dictionary<Vector3Int, HoedTileData> HoedTiles
    {
        get { return _hoedTiles; }
        private set { _hoedTiles = value; }
    }
   
    private Dictionary<Vector3Int, WateredTileData> _wateredTiles = new Dictionary<Vector3Int, WateredTileData>();
    public Dictionary<Vector3Int, WateredTileData> WateredTiles
    {
        get { return _wateredTiles; }
        private set { _wateredTiles = value; }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAllTileStatus(int minute)
    {
        foreach (var hoedTile in HoedTiles.ToList())
        {
            Vector3Int hoedPosition = hoedTile.Key;
            HoedTileData hoedTileData = hoedTile.Value;

            if (WateredTiles.ContainsKey(hoedPosition) || CropManager.Instance.PlantedCrops.ContainsKey(hoedPosition))
            {
                hoedTileData.hasSomethingOn = true;
            }
            hoedTileData.CheckTile(minute);
            if (hoedTileData.needRemove)
            {
                RemoveHoedTile(hoedPosition);
            }
             
        }

        foreach (var wateredTile in WateredTiles.ToList())
        {
            Vector3Int wateredPosition = wateredTile.Key;
            WateredTileData wateredTileData = wateredTile.Value;


            wateredTileData.CheckTile(minute);
            if (wateredTileData.needRemove)
            {
                RemoveWateredTile(wateredPosition);
            }

        }
    }

    public void AddHoedTile(Vector3Int tilePos)
    {
        HoedTileData newHoedTile = new HoedTileData();
        _hoedTiles.Add(tilePos, newHoedTile);

        Debug.Log($"add hoed tile at {tilePos}");
    }

    public void RemoveHoedTile(Vector3Int tilePos)
    {
        _hoedTilemap.SetTile(tilePos, null);
        HoedTiles.Remove(tilePos);
        if(WateredTiles.ContainsKey(tilePos)) RemoveWateredTile(tilePos);

        Debug.Log($"removed hoed tile at {tilePos}");
    }


    public void AddWateredTile(Vector3Int tilePos)
    {
        WateredTileData newWateredTile = new WateredTileData();
        _wateredTiles.Add(tilePos, newWateredTile);
        Debug.Log($"add watered tile at {tilePos}");
    }

    public void RemoveWateredTile(Vector3Int tilePos)
    {
        _wateredTilemap.SetTile(tilePos, null);
        WateredTiles.Remove(tilePos);

        Debug.Log($"removed watered tile at {tilePos}");
    }
}
