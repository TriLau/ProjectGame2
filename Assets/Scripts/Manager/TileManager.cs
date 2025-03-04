using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    [SerializeField]
    private List<Vector3Int> _hoeTiles = new List<Vector3Int>();
    public List<Vector3Int> HoeTiles
    {
        get { return _hoeTiles; }
        set { _hoeTiles = value; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHoeTile(Vector3Int tilePos)
    {
        HoeTiles.Add(tilePos);
    }
}
