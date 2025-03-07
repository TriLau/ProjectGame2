using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldManager : Singleton<ItemWorldManager>, IDataPersistence
{
    private ListItemWorld _listItemWorld;
    public GameObject itemPrefab;
    public ItemDatabase itemDatabase;
    public ItemWorldControl[] itemsOnMap;

    void Start()
    {
        itemsOnMap = FindObjectsOfType<ItemWorldControl>();

        foreach (var item in itemsOnMap)
        {
            ItemWorld itemWorld = item.GetItemWorld();
            _listItemWorld.AddItemWorld(itemWorld);
        }

        //foreach (var item in itemsOnMap)
        //{
        //    Destroy(item.gameObject);
        //}

        //SpawnItem();
    }

    void Update()
    {
        
    }

    public void SpawnItem()
    {
        foreach (var item in _listItemWorld.Items)
        {
            GameObject itemGO = Instantiate(itemPrefab, item.Value.Position, Quaternion.identity);
            ItemWorldControl itemWorldControl = itemGO.GetComponent<ItemWorldControl>();
            itemWorldControl.SetItemWorld(item.Value);
        }
    }

    public void AddItemWorld(ItemWorld item)
    {
        _listItemWorld.AddItemWorld(item);
    }

    public void RemoveItemWorld(ItemWorld item)
    {
        _listItemWorld.RemoveItemWorld(item);
    }
    
    public void LoadData(GameData gameData)
    {
        _listItemWorld = gameData.ListItemWold;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.SetListItemWorld(_listItemWorld);
    }
}
