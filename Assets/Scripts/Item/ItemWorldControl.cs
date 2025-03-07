using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldControl : MonoBehaviour
{
    public string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }
    public Item item;
    private ItemWorld _itemWorld;

    private void Awake() // xai tam cai nay de test spawn item
    {
        InitialItem(item);
    }

    private void Start()
    {
        _itemWorld = new ItemWorld(id, item, 1, transform.position);
    }

    private void Update()
    {
        transform.gameObject.GetComponent<Rigidbody2D>().velocity *= 0.8f;
    }

    public void InitialItem(Item item)
    {
        GetComponent<SpriteRenderer>().sprite = item.image;
    }

    public ItemWorld GetItemWorld()
    {
        return _itemWorld;
    }

    public void SetItemWorld(ItemWorld itemWorld)
    {
        _itemWorld = itemWorld;
        id = itemWorld.Id;
        item = itemWorld.Item;
        InitialItem(item);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (InventoryManager.Instance.AddItemToInventorySlot(_itemWorld))
            {
                _itemWorld.SetColected(true);
                ItemWorldManager.Instance.RemoveItemWorld(_itemWorld);
                Destroy(gameObject);
            }
        }
    }
}
