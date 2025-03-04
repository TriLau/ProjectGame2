using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldManager : Singleton<ItemWorldManager>
{
    [SerializeField]
    private List<GameObject> _itemWorlds = new List<GameObject>();
    public List<GameObject> ItemWorlds
    {
        get { return _itemWorlds; }
        set { _itemWorlds = value; }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItemWorld(GameObject item)
    {
        ItemWorlds.Add(item);
    }

    public void RemoveItemWorld(GameObject item)
    {
        ItemWorlds.Remove(item);
    }
}
